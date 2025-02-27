using Option.OptionType;
using Shouldly;

namespace OptionTests.OptionType;

public class NoneTests
{
    [Fact]
    public void None_ShouldMatchNone()
    {
        // Arrange.
        None<int> option = Option<int>.None();
        bool noneMatched = false;
        
        // Act.
        option.Match(() => noneMatched = true, _ => { }, _ => { });
        
        // Assert.
        noneMatched.ShouldBeTrue();
    }
}
