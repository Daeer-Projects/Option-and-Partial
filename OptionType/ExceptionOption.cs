namespace OptionType;

public sealed class ExceptionOption<T>(Exception exception) : Option
{
    public Exception Exception { get; } = exception;
    
    // Implicit conversion to Exception, allowing direct pattern matching
    public static implicit operator Exception(ExceptionOption<T> exOption) => exOption.Exception;
}
