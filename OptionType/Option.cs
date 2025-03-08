namespace OptionType;

public abstract class Option
{
    public static Some<T> Some<T>(T value) => new(value);
    public static None<T> None<T>() => new();
    public static Option From<T>(T value) => value is null ? None<T>() : Some<T>(value);
}
