namespace Option.OptionType;

public sealed class Some<T>(T value) : Option<T>
{
    public override void Match(Action onNone, Action<T> onSome, Action<Exception> onException) => onSome(value);

    public override TResult Match<TResult>(
        Func<TResult> onNone, Func<T, TResult> onSome, Func<Exception, TResult> onException) =>
        onSome(value);

    public override Option<TResult> Join<TResult>(Func<T, Option<TResult>> binder) => binder(value);
}
