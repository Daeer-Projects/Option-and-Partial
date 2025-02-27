using Option.OptionType;
using Shouldly;

namespace OptionTests.OptionType;

public class SomeTests
{
    [Fact]
    public void Some_ShouldContainValue()
    {
        // Arrange.
        Some<int> option = Option<int>.Some(42);
        
        // Act & Assert.
        option.Match(
            onSome: value => value.ShouldBe(42),
            onNone: () => throw new Exception("Expected Some, got None"),
            onException: _ => throw new Exception("Expected Some, got Exception")
        );
    }
}
