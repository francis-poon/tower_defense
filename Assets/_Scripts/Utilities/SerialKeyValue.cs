using System;

[Serializable]
public class SerialKeyValue<TKey, TValue>
{
    public TKey Key;
    public TValue Value;

    public SerialKeyValue(TKey _key, TValue _value)
    {
        this.Key = _key;
        this.Value = _value;
    }
}
