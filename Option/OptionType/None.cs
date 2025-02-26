namespace Option.OptionType;

public sealed class None<T> : Option<T>
{
    public override void Match(Action onNone, Action<T> onSome, Action<Exception> onException) => onNone();

    public override TResult Match<TResult>(
        Func<TResult> onNone, Func<T, TResult> onSome, Func<Exception, TResult> onException) =>
        onNone();

    public override Option<TResult> Join<TResult>(Func<T, Option<TResult>> binder) => new None<TResult>();
}
