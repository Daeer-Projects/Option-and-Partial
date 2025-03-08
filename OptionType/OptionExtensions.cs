namespace OptionType;

public static class OptionExtensions
{
    public static Option ToOption<T>(this T value) => Option.From(value);
}
