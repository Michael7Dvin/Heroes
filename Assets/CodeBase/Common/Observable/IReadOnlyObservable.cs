using System;

namespace CodeBase.Common.Observable
{
    public interface IReadOnlyObservable<out T>
    {
        event Action<T> Changed;
        T Value { get; }    
    }
}