# Option and Partial

 A repo for an experiment with DUs and partial functions.

 ## Purpose

 The main reason for this repo is to experiment with Discriminated Unions (DU). C# doesn't have them yet, but they are on the way.

 I was curious how my code would change, or benefit, from using DUs. To experiment, I am creating this repo to see how I would make a library that allowed me to use DUs and partial functions.

 The partial functions are to help me consume the `Option<T>` class. How do I handle the `Some` and `None` flow? Thinking about this made me want to use something more fluent without a lot of nested switch statements.

## Road Map

I'm just starting this, so it is still up in the air at the moment and will change as I figure out what I want from the repo.

* Initial concept.
* Add unit tests.
* Understand what I want.

## How to use

I guess the only way to show how to use the classes is to write unit tests. With unit tests, you get to see how I envisage the usage of the classes and extensions.

### Current usage

Say we have three `Option<T>` objects and we want to check that all three have a `Some<T>` and then execute a method that requires all three. Rather than check each version has a `Some<T>` value, we could use a fluent api to check the variables and pass them to the main method. The issue there, would be what if any of those variables was a `None<T>`?

The first thing I'm going to do, is take the first variable and make it a `MultiOption`. The `MultiOption` class is where we store the values, nulls or exceptions.

`variableOne.Bind()`

Now we have the `MultiOption` class setup, we can now join the next two variables to the `MultiOption`.

```csharp
variableOne
    .Bind()
    .Join(variableTwo)
    .Join(variableThree)
```

If all has gone well and the three variables have a `Some<T>`, we can now call the method using the `Map()` call. The `Map()` method is contained in the `MultiOption` class.

The method we are calling could be doing something like logging, or setting up another variable or calling off to another service. The important thing is that this method takes in an array of `object` and a count of variables in the array.

At the moment, it doesn't matter what it returns, but I would like to figure that out later. This execution has side effects. It is not functional. I would like to change this when I figure out how to do it better.

```csharp
variableOne
    .Bind()
    .Join(variableTwo)
    .Join(variableThree)
    .Map(ProcessThreeVariablesMethod)
```

So, if everything went well, what is done in the `ProcessThreeVariablesMethod` works.

The next things we need to do, is handle what happens if any of the variables has a `None<T>` or `ExceptionOption<T>`. To do that, I've added a `Fallback()` method to the `Option` class. More than likely, this is going to be used for logging an error and sending the flow in another direction.

This is the full code with the fallback.

```csharp
variableOne
    .Bind()
    .Join(variableTwo)
    .Join(variableThree)
    .Map(ProcessThreeVariablesMethod)
    .Fallback(exception => logger.LogError($"Something went wrong. Error: {exception}"));
```

### Partial functions

I have created extension methods to return partial methods. However, I've not used them yet.

At the moment, I'm not sure what method is better. Until I'm more sure of what I want to achieve, I'll keep both route open.

## Problems to Solve

### MultiOption

In the `Map` function, the exceptions should be returned as a single exception if there is only one or an aggregate exception with the full list.

### Async Versions

The `async` versions don't work or seem very `async`.

### Is there a point to this

Looking at what I have created and how it is used, I'm facing the question, 'What is the point of discriminated unions?'

I'm sure they work well in the languages that they exist in currently, but do they work in C#? What are the expected usages of them in the language and have they decided how they will be consumed?

I mean, isn't it easier to just use if's?

```csharp
if (variableOne.HasValue)
{
    if (variableTwo.HasValue)
    {
        if (variableThree.HasValue)
        {
            ProcessThreeVariablesMethod(variableOne, variableTwo, variableThree);
            return;
        }
    }
}

logger.LogError($"Something went wrong. Error: {exception}")
return;
```

I've raised a comment on the `dotnet/csharplang` repo in the discussion about the type unions.

[Type Union Discussion](https://github.com/dotnet/csharplang/discussions/8313?sort=new)

What is the proposal to check all three variables are of type, `Some`?

I'm not liking the idea of these ideas.

```csharp
Option<int> variableOne = serviceOne.GetOne();
Option<int> variableTwo = serviceTwo.GetTwo();
Option<int> variableThree = serviceThree.GetThree();
int total = 0;

if (variableOne is Some)
{
    if (variableTwo is Some)
    {
        if (variableThree is Some)
        {
            total = SumValues(variableOne, variableTwo, variableThree)
        }
    }
}

return;

int SumValues(int a, int b, int c)
{
    return a + b + c;
}

```

Or this:

```csharp
Option<int> variableOne = serviceOne.GetOne();
Option<int> variableTwo = serviceTwo.GetTwo();
Option<int> variableThree = serviceThree.GetThree();

int total = variableOne switch
{
    Some: value => 
    {
        variableTwo switch
        {
            Some: valueTwo =>
            {
                variableThree switch
                {
                    Some: valueThree => 
                    {
                        return SumValues(valueOne, valueTwo, valueThree);
                    },
                    None: => 0;
                }
            },
            None: => 0
        }
    },
    None: => 0
};


return;

int SumValues(int a, int b, int c)
{
    return a + b + c;
}
```

I'm curious on how this is going to look. Has any thought gone into how we will consume the `Option<TValue>`?

The reply from the repo is this:

```csharp
Option<int> variableOne = serviceOne.GetOne();
Option<int> variableTwo = serviceTwo.GetTwo();
Option<int> variableThree = serviceThree.GetThree();

int total = (variableOne, variableTwo, variableThree) switch
{
    (Some(var valueOne), Some(var valueTwo), Some(var valueThree)) => SumValues(valueOne, valueTwo, valueThree),
    _ => 0
}

return;
int SumValues(int a, int b, int c)
{
    return a + b + c;
}
```

Or:

```csharp
Option<int> variableOne = serviceOne.GetOne();
Option<int> variableTwo = serviceTwo.GetTwo();
Option<int> variableThree = serviceThree.GetThree();
int total = 0;

if ((variableOne, variableTwo, variableThree) is (Some(var a), Some(var b), Some(var c)))
{
    total = SumValues(a, b, c);
}

return;
int SumValues(int a, int b, int c)
{
    return a + b + c;
}
```

#### Is this better

The question now becomes, which is the better way to consume the `Option<T>`?

I've created an experiment test to see if this works with what I have and they look like this:

```csharp
    [Fact]
    public void Experiment_ToSeeWhatHappens_WithValues_ShouldReturnExpected()
    {
        // Arrange.
        const int expected = 42;
        Option<int> valOne = Option<int>.Some(10);
        Option<int> valTwo = Option<int>.Some(20);
        Option<int> valThree = Option<int>.Some(12);
        
        // Act.
        int total = (valOne, valTwo, valThree) switch
        {
            (Some<int> one, Some<int> two, Some<int> three) => SumValues([one.Value, two.Value, three.Value]),
            _ => 0
        };
        
        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void Experiment_ToSeeWhatHappens_WithNone_ShouldReturnZero()
    {
        // Arrange.
        const int expected = 0;
        Option<int> valOne = Option<int>.Some(10);
        Option<int> valTwo = Option<int>.None();
        Option<int> valThree = Option<int>.Some(12);
        
        // Act.
        int total = (valOne, valTwo, valThree) switch
        {
            (Some<int> one, Some<int> two, Some<int> three) => SumValues([one.Value, two.Value, three.Value]),
            _ => 0
        };
        
        // Assert.
        total.ShouldBe(expected);
    }
    
    private static int SumValues(int[] values)
    {
        return values.Sum();
    }
```

They work and pass. What I don't like is the `Option<int>.Some(10)` which returns a `Option<int> val`. Having the generic `<T>` on the `Option` class makes this look ugly. Can I push it down to the child classes?

That experiment went a bit disastrously.
