namespace OptionType;

public sealed class Some<T>(T value) : Option
{
    public T Value { get; } = value;
}
