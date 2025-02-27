using Option.OptionType;
using Shouldly;

namespace OptionTests.OptionType;

public class OptionTests
{
    [Fact]
    public void None_ShouldBeOfTypeNone()
    {
        // Arrange.
        None<int> option = Option<int>.None();
        
        // Act.
        // Assert.
        option.ShouldBeOfType<None<int>>();
    }
    
    [Fact]
    public void Some_ShouldBeOfTypeSome()
    {
        // Arrange.
        Some<int> option = Option<int>.Some(42);
        
        // Act.
        // Assert.
        option.ShouldBeOfType<Some<int>>();
    }
    
    [Fact]
    public void Exception_ShouldBeOfTypeException()
    {
        // Arrange.
        ExceptionOption<int> option = Option<int>.Exception(new NullReferenceException("An exception occurred."));
        
        // Act.
        // Assert.
        option.ShouldBeOfType<ExceptionOption<int>>();
    }
    
    [Fact]
    public void From_ShouldBeOfTypeSome_WithValue()
    {
        // Arrange.
        Option<int> option = Option<int>.From(42);
        
        // Act.
        // Assert.
        option.ShouldBeOfType<Some<int>>();
    }
    
    [Fact]
    public void From_ShouldBeOfTypeNone_WithNull()
    {
        // Arrange.
        Option<int?> option = Option<int?>.From(null);
        
        // Act.
        // Assert.
        option.ShouldBeOfType<None<int?>>();
    }
}
