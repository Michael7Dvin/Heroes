namespace CodeBase.Common.Referable
{
    public interface IReadOnlyReferable<out T>
    {
        T Value { get; }
    }
}