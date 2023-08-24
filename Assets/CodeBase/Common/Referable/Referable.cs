namespace CodeBase.Common.Referable
{
    public class Referable<T> : IReadOnlyReferable<T>
    {
        public Referable()
        {
        }

        public Referable(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}