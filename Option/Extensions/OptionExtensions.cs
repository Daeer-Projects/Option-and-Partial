using Option.OptionType;

namespace Option.Extensions;

public static class OptionExtensions
{
    public static Option<T> ToSome<T>(this T value) => new Some<T>(value);

    public static Option<T> ToNone<T>() => new None<T>();

    public static Option<T> ToOption<T>(this T value) => Option<T>.From(value);

    public static MultiOption Bind<T>(this Option<T> option) => MultiOption.Empty.Join(option);

    public static MultiOption Join<T>(this MultiOption multiOption, Option<T> option) => multiOption.Join(option);

    public static async Task<MultiOption> JoinAsync<T>(this Task<Option<T>> taskOption) =>
        await MultiOption.Empty.JoinAsync(taskOption);

    public static async Task<MultiOption> JoinAsync<T>(this MultiOption multiOption, Task<Option<T>> taskOption) =>
        await multiOption.JoinAsync(taskOption);

    public static Func<T2, TResult> Partial<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1) =>
        arg2 => func(arg1, arg2);

    public static Func<T2, Func<T3, TResult>> Partial<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1) =>
        arg2 => arg3 => func(arg1, arg2, arg3);

    public static Func<T2, Func<T3, Func<T4, TResult>>> Partial<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 arg1) =>
        arg2 => arg3 => arg4 => func(arg1, arg2, arg3, arg4);
}
