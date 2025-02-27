using Option.OptionType;
using Shouldly;

namespace OptionTests.OptionType;

public class ExceptionTests
{
    [Fact]
    public void Exception_ShouldMatchException()
    {
        // Arrange.
        InvalidOperationException exception = new("Test exception");
        ExceptionOption<int> option = Option<int>.Exception(exception);
        
        // Act & Assert.
        option.Match(
            onSome: _ => throw new Exception("Expected Exception, got Some"),
            onNone: () => throw new Exception("Expected Exception, got None"),
            onException: ex => ex.ShouldBe(exception)
        );
    }
}
