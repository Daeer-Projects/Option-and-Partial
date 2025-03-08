using OptionType;
using Shouldly;

namespace OptionTypeTests.Integration;

public class IntTests
{
    [Fact]
    public void ToOption_WithMatch_ShouldReturnExpected()
    {
        // Arrange.
        const int expected = 42;
        Option valOne = expected.ToOption();
        
        // Act.
        int total = valOne switch
        {
            Some<int> one => one.Value,
            _ => 0
        };
        
        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void ToOption_WithNoMatch_ShouldReturnZero()
    {
        // Arrange.
        const int expected = 0;
        int? input = null;
        Option valOne = input.ToOption();
        
        // Act.
        int total = valOne switch
        {
            Some<int> one => one.Value,
            _ => 0
        };
        
        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void ToOption_WithMatchAndSum_ShouldReturnExpected()
    {
        // Arrange.
        const int expected = 42;
        Option valOne = 10.ToOption();
        Option valTwo = 20.ToOption();
        Option valThree = 12.ToOption();
        
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
    public void ToOption_WithNone_ShouldReturnZero()
    {
        // Arrange.
        const int expected = 0;
        int? inputValueTwo = null;
        Option valOne = 10.ToOption();
        Option valTwo = inputValueTwo.ToOption();
        Option valThree = 12.ToOption();
    
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
    public void ToOption_WithSome_ShouldReturnTrue()
    {
        // Arrange.
        Option valOne = 42.ToOption();

        // Act.
        bool methodCalled = valOne switch
        {
            Some<int> => true,
            _ => false
        };

        // Assert.
        methodCalled.ShouldBeTrue();
    }
    
    [Fact]
    public void ToOption_WithSome_ShouldMutateValueWithExternalMethod()
    {
        // Arrange.
        Option valOne = 42.ToOption();
        string message = string.Empty;

        // Act.
        switch (valOne)
        {
            case Some<int>:
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
        int? inputValue = null;
        Option valOne = inputValue.ToOption();

        // Act.
        bool methodCalled = valOne switch
        {
            Some<int> => true,
            _ => false
        };

        // Assert.
        methodCalled.ShouldBeFalse();
    }
    
    [Fact]
    public void ToOption_WithNone_ShouldNotMutateValueWithExternalMethod()
    {
        // Arrange.
        int? inputValue = null;
        Option valOne = inputValue.ToOption();
        string message = string.Empty;

        // Act.
        switch (valOne)
        {
            case Some<int>:
                SetToTrue(ref message);
                break;
            default:
                message = string.Empty;
                break;
        }
        
        // Assert.
        message.ShouldBeEmpty();
    }
    
    private static int SumValues(int[] values) => values.Sum();
    
    // ReSharper disable once RedundantAssignment
    private static void SetToTrue(ref string message)
    {
        message = "Method was called.";
    }
}
