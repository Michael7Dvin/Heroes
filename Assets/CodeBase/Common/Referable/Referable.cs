namespace CodeBase.Common.Referable
{
    public class Referable<T> : IReadOnlyReferable<T>
    {
        public T Value { get; set; }
    }
}