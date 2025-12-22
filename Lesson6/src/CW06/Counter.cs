namespace CW06;

public sealed class Counter
{
    public int Value { get; private set; }

    public event EventHandler<int>? Changed;

    public void Increment()
    {
        Value++;
        Changed?.Invoke(this, Value);
    }
}
