using System;
using CodeBase.Common.Referable;

namespace CodeBase.Common.Observable
{
    public interface IReadOnlyObservable<out T> : IReadOnlyReferable<T>
    {
        event Action<T> Changed;
    }
}