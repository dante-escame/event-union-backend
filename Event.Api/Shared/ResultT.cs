namespace Event.Api.Shared;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool succeded, Error error) : base(succeded, error) => _value = value;

    public TValue Value => Succeded ? _value! : throw new InvalidOperationException("Dados de resultados falhos não podem ser acessados.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}