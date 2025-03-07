namespace OptionType;

public sealed class ExceptionOption<T>(Exception exception) : Option
{
    public Exception Exception { get; } = exception;
    
    // Enables pattern matching: (ExceptionOption<int>(Exception ex))
    public void Deconstruct(out Exception exception) => exception = Exception;
}
