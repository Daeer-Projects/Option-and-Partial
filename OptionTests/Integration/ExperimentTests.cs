using Option.OptionType;
using Shouldly;

namespace OptionTests.Integration;

public class ExperimentTests
{
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
}
