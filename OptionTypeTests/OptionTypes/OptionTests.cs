using OptionType;
using Shouldly;

namespace OptionTypeTests.OptionTypes;

public class OptionTests
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
    public void Exception_ShouldBeOfTypeException()
    {
        // Arrange.
        ExceptionOption<int> option = Option.FromException<int>(new NullReferenceException("An exception occurred."));
        
        // Act.
        // Assert.
        option.ShouldBeOfType<ExceptionOption<int>>();
    }
    
    [Fact]
    public void From_ShouldBeOfTypeSome_WithValue()
    {
        // Arrange.
        Option option = Option.From(42);
        
        // Act.
        // Assert.
        option.ShouldBeOfType<Some<int>>();
    }
    
    [Fact]
    public void From_ShouldBeOfTypeNone_WithNull()
    {
        // Arrange.
        Option option = Option.From<int?>(null);
        
        // Act.
        // Assert.
        option.ShouldBeOfType<None<int?>>();
    }
}
