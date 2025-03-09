using OptionType;
using Shouldly;

namespace OptionTypeTests.OptionTypes;

public class SomeTests
{
    [Fact]
    public void Some_ShouldBeOfTypeSome()
    {
        // Arrange.
        Some<int> option = Option.Some(42);
        
        // Act.
        // Assert.
        option.ShouldBeOfType<Some<int>>();
    }
    
    [Fact]
    public void Some_ValueShouldBeExpected()
    {
        // Arrange.
        const int expected = 42;
        Some<int> option = Option.Some(expected);
        
        // Act.
        // Assert.
        option.Value.ShouldBe(expected);
    }
}
