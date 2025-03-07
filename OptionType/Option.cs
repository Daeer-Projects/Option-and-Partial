namespace OptionType;

public abstract class Option
{
    public static Some<T> Some<T>(T value) => new(value);
    public static None<T> None<T>() => new();
    public static ExceptionOption<T> FromException<T>(Exception ex) => new(ex);
}
