using OptionType;
using Shouldly;

namespace OptionTypeTests.Integration;

public class ExperimentalTests
{
    [Fact]
    public void Experiment_ToSeeWhatHappens_WithValues_ShouldReturnExpected()
    {
        // Arrange.
        const int expected = 42;
        Option valOne = Option.Some(10);
        Option valTwo = Option.Some(20);
        Option valThree = Option.Some(12);
        
        // Act.
        int total = (valOne, valTwo, valThree) switch
        {
            (Some<int> one, Some<int> two, Some<int>three) => SumValues([one.Value, two.Value, three.Value]),
            (ExceptionOption<int> ex, _, _) => HandleException(ex),
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
        Option valOne = Option.Some(10);
        Option valTwo = Option.None<int>();
        Option valThree = Option.Some(12);
    
        // Act.
        int total = (valOne, valTwo, valThree) switch
        {
            (Some<int> one, Some<int> two, Some<int> three) => SumValues([one.Value, two.Value, three.Value]),
            (ExceptionOption<int> ex, _, _) => HandleException(ex),
            _ => 0
        };
    
        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void Experiment_ToSeeWhatHappens_WithException_ShouldReturnZero()
    {
        // Arrange.
        const int expected = 0;
        Option valOne = Option.Some(10);
        Option valTwo = Option.FromException<int>(new InvalidOperationException("Something went wrong"));
        Option valThree = Option.Some(12);

        // Act.
        int total = (valOne, valTwo, valThree) switch
        {
            (Some<int> one, Some<int> two, Some<int> three) => SumValues([one.Value, two.Value, three.Value]),
            (ExceptionOption<int> ex, _, _) => HandleException(ex),
            _ => 0
        };

        // Assert.
        total.ShouldBe(expected);
    }
    
    private static int SumValues(int[] values) => values.Sum();
    private static int HandleException(Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        return 0;
    }
}
