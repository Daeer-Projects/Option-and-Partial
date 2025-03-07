namespace OptionType;

public sealed class None<T> : Option
{
    // Enables pattern matching: (None<int>())
    public void Deconstruct() { }
}
