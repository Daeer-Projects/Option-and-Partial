namespace Option.OptionType;

public sealed class MultiOption
{
    private MultiOption(IEnumerable<object> values, IEnumerable<Exception> exceptions)
    {
        this.values.AddRange(values);
        this.exceptions.AddRange(exceptions);
    }

    private readonly List<Exception> exceptions = [];
    private readonly List<object> values = [];

    public static MultiOption Empty => new([], []);

    public MultiOption Join<T>(Option<T> option)
    {
        return option.Match(
            onNone: () => this,
            onSome: value => new MultiOption(values.Append(value!), exceptions),
            onException: ex => new MultiOption(values, exceptions.Append(ex))
        );
    }

    public async Task<MultiOption> JoinAsync<T>(Task<Option<T>> taskOption)
    {
        Option<T> option = await taskOption;
        return Join(option);
    }

    public Option<TResult> Map<TResult>(Func<object[], int, TResult> func) =>
        exceptions.Count != 0
            ? Option<TResult>.Exception(exceptions.First())
            : Option<TResult>.Some(func(values.ToArray(), values.Count));

    public async Task<Option<TResult>> MapAsync<TResult>(Func<object[], Task<TResult>> func) =>
        exceptions.Count != 0
            ? Option<TResult>.Exception(exceptions.First())
            : Option<TResult>.Some(await func(values.ToArray()));
}
