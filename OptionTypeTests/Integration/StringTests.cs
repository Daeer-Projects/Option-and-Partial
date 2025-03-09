using OptionType;
using Shouldly;

namespace OptionTypeTests.Integration;

public class StringTests
{
    
    [Fact]
    public void ToOption_WithMatch_ShouldReturnExpected()
    {
        // Arrange.
        const string expected = "Hello";
        Option valOne = expected.ToOption();
        
        // Act.
        string total = valOne switch
        {
            Some<string> one => one.Value,
            _ => string.Empty
        };
        
        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void ToOption_WithNoMatch_ShouldReturnZero()
    {
        // Arrange.
        string? input = null;
        Option valOne = input.ToOption();
        
        // Act.
        string total = valOne switch
        {
            Some<string> one => one.Value,
            _ => string.Empty
        };
        
        // Assert.
        total.ShouldBeEmpty();
    }
    
    [Fact]
    public void ToOption_WithMatchAndSum_ShouldReturnExpected()
    {
        // Arrange.
        const string expected = "Hello to the world!";
        Option valOne = "Hello ".ToOption();
        Option valTwo = "to the ".ToOption();
        Option valThree = "world!".ToOption();
        
        // Act.
        string total = (valOne, valTwo, valThree) switch
        {
            (Some<string> one, Some<string> two, Some<string> three) => SumValues([one.Value, two.Value, three.Value]),
            _ => string.Empty
        };
        
        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void ToOption_WithNone_ShouldReturnZero()
    {
        // Arrange.
        string? inputValueTwo = null;
        Option valOne = "Hello".ToOption();
        Option valTwo = inputValueTwo.ToOption();
        Option valThree = "world!".ToOption();
    
        // Act.
        string total = (valOne, valTwo, valThree) switch
        {
            (Some<string> one, Some<string> two, Some<string> three) => SumValues([one.Value, two.Value, three.Value]),
            _ => string.Empty
        };
    
        // Assert.
        total.ShouldBeEmpty();
    }
    
    [Fact]
    public void ToOption_WithSome_ShouldReturnTrue()
    {
        // Arrange.
        Option valOne = "Hello".ToOption();

        // Act.
        bool methodCalled = valOne switch
        {
            Some<string> => true,
            _ => false
        };

        // Assert.
        methodCalled.ShouldBeTrue();
    }
    
    [Fact]
    public void ToOption_WithSome_ShouldMutateValueWithExternalMethod()
    {
        // Arrange.
        Option valOne = "Hello".ToOption();
        string message = string.Empty;

        // Act.
        switch (valOne)
        {
            case Some<string>:
                SetToTrue(ref message);
                break;
            default:
                message = string.Empty;
                break;
        }
        
        // Assert.
        message.ShouldNotBeEmpty();
    }
    
    [Fact]
    public void ToOption_WithNone_ShouldReturnFalse()
    {
        // Arrange.
        string? inputValue = null;
        Option valOne = inputValue.ToOption();

        // Act.
        bool methodCalled = valOne switch
        {
            Some<string> => true,
            _ => false
        };

        // Assert.
        methodCalled.ShouldBeFalse();
    }
    
    [Fact]
    public void ToOption_WithNone_ShouldNotMutateValueWithExternalMethod()
    {
        // Arrange.
        string? inputValue = null;
        Option valOne = inputValue.ToOption();
        string message = string.Empty;

        // Act.
        switch (valOne)
        {
            case Some<string>:
                SetToTrue(ref message);
                break;
            default:
                message = string.Empty;
                break;
        }
        
        // Assert.
        message.ShouldBeEmpty();
    }
    
    private static string SumValues(string[] values) => $"{values[0]}{values[1]}{values[2]}";
    
    // ReSharper disable once RedundantAssignment
    private static void SetToTrue(ref string message)
    {
        message = "Method was called.";
    }
}
