namespace Infrastructure
{
    public class MutableKeyValuePair<KeyType, ValueType>
    {
        public KeyType Key { get; }
        public ValueType Value { get; set; }

        public MutableKeyValuePair() { }

        public MutableKeyValuePair(KeyType key, ValueType val)
        {
            Key = key;
            Value = val;
        }
    }
}