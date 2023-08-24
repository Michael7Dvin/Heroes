using System;

namespace CodeBase.Common.Observable
{
    public class Observable<T> : IReadOnlyObservable<T>
    {
        private T _value;

        public Observable()
        {
        }

        public Observable(T value)
        {
            _value = value;
        }

        public event Action<T> Changed;

        public T Value
        {
            get => _value;

            set
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }
    }
}