namespace OptionType;

public static class OptionExtensions
{
    public static Option ToOption<T>(this T value) => Option.From(value);
    
    public static Func<T2, TResult> Partial<T1, T2, TResult>(this T1 arg1, Func<T1, T2, TResult> func) =>
        arg2 => func(arg1, arg2);
    
    public static Func<T2, Func<T3, TResult>> Partial<T1, T2, T3, TResult>(this T1 arg1, Func<T1, T2, T3, TResult> func) =>
        arg2 => arg3 => func(arg1, arg2, arg3);
    
    public static Func<T3, TResult> Partial<T2, T3, TResult>(this Func<T2, Func<T3, TResult>> func, T2 arg2) =>
        arg3 => func(arg2)(arg3);
    
    public static Func<T2, Func<T3, Func<T4, TResult>>> Partial<T1, T2, T3, T4, TResult>(this T1 arg1, Func<T1, T2, T3, T4, TResult> func) =>
        arg2 => arg3 => arg4 => func(arg1, arg2, arg3, arg4);
    
    public static Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>> Partial<T1, T2, T3, T4, T5, TResult>(this T1 arg1, Func<T1, T2, T3, T4, T5, TResult> func) =>
        arg2 => arg3 => arg4 => arg5 => func(arg1, arg2, arg3, arg4, arg5);
}
