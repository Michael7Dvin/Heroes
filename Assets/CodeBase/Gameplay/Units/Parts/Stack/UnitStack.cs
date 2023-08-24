using CodeBase.Common.Observable;

namespace CodeBase.Gameplay.Units.Parts.Stack
{
    public class UnitStack
    {
        private readonly Observable<int> _amount = new();

        public UnitStack(int amount)
        {
            _amount.Value = amount;
        }

        public IReadOnlyObservable<int> Amount => _amount;

        public void SetAmount(int amount) => 
            _amount.Value = amount;
    }
}