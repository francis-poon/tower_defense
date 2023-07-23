using System;

[Serializable]
public class SerialKeyValue<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public SerialKeyValue(TKey _key, TValue _value)
    {
        this.key = _key;
        this.value = _value;
    }
}
