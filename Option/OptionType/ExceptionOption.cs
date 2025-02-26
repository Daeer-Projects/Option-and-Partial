namespace Option.OptionType;

public sealed class ExceptionOption<T>(Exception exception) : Option<T>
{
    public Exception ExceptionCaught => exception;

    public override void Match(Action onNone, Action<T> onSome, Action<Exception> onException) =>
        onException(exception);

    public override TResult Match<TResult>(
        Func<TResult> onNone, Func<T, TResult> onSome, Func<Exception, TResult> onException)
        => onException(exception);

    public override Option<TResult> Join<TResult>(Func<T, Option<TResult>> binder) =>
        new ExceptionOption<TResult>(exception);
}
