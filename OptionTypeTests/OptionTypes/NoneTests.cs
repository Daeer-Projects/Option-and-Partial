using OptionType;
using Shouldly;

namespace OptionTypeTests.OptionTypes;

public class NoneTests
{
    [Fact]
    public void None_ShouldBeOfTypeNone()
    {
        // Arrange.
        None<int> option = Option.None<int>();
        
        // Act.
        // Assert.
        option.ShouldBeOfType<None<int>>();
    }
}
