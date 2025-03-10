using OptionType;
using Shouldly;

namespace OptionTypeTests.Extensions;

public class OptionExtensionTests
{
    [Fact]
    public void ToOption_WithValueShouldBeOfTypeSome()
    {
        // Arrange.
        Option option = 42.ToOption();
        
        // Act.
        // Assert.
        option.ShouldBeOfType<Some<int>>();
    }
    
    [Fact]
    public void ToOption_WithNullShouldBeOfTypeNone()
    {
        // Arrange.
        int? input = null;
        Option option = input.ToOption();
        
        // Act.
        // Assert.
        option.ShouldBeOfType<None<int?>>();
    }
}
