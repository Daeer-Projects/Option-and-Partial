namespace Option.OptionType;

public abstract class Option<T>
{
    public static None<T> None() => new();

    public static Some<T> Some(T value) => new(value);

    public static Option<T> From(T value) => value is null ? None() : Some(value);

    public static ExceptionOption<T> Exception(Exception exception) => new(exception);

    public abstract void Match(Action onNone, Action<T> onSome, Action<Exception> onException);

    public abstract TResult Match<TResult>(
        Func<TResult> onNone, Func<T, TResult> onSome, Func<Exception, TResult> onException);

    public abstract Option<TResult> Join<TResult>(Func<T, Option<TResult>> binder);

    public void Fallback(Action<Exception?> errorHandler)
    {
        if (this is ExceptionOption<T> exceptionOpt)
        {
            errorHandler(exceptionOpt.ExceptionCaught);
        }
    }

    public async Task FallbackAsync(Func<Exception?, Task> errorHandler)
    {
        if (this is ExceptionOption<T> exceptionOpt)
        {
            await errorHandler(exceptionOpt.ExceptionCaught);
        }
    }
}
