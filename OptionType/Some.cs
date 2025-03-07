namespace OptionType;

public sealed class Some<T>(T value) : Option
{
    public T Value { get; } = value;

    // Enables pattern matching: (Some<int>(int value))
    public void Deconstruct(out T value) => value = Value;
}
