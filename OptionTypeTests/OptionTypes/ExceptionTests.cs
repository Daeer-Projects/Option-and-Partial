using OptionType;
using Shouldly;

namespace OptionTypeTests.OptionTypes;

public class ExceptionTests
{
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
    public void Exception_ShouldBeOfTypeExpected()
    {
        // Arrange.
        ExceptionOption<int> option = Option.FromException<int>(new NullReferenceException("An exception occurred."));
        
        // Act.
        // Assert.
        option.Exception.ShouldBeOfType<NullReferenceException>().Message.ShouldBe("An exception occurred.");
    }
}
