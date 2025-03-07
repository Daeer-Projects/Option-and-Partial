namespace OptionType;

public static class OptionExtensions
{
    public static Option ToSome<T>(this T value) => new Some<T>(value);

    public static Option ToNone<T>(this T _) => new None<T>();

    public static Option ToOption<T>(this T value) => Option.From(value);
}
