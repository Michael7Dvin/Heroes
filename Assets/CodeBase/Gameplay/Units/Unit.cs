using CodeBase.Gameplay.Units.Logic;
using CodeBase.Gameplay.Units.View;

namespace CodeBase.Gameplay.Units
{
    public class Unit
    {
        public Unit(UnitLogic logic, UnitView view)
        {
            Logic = logic;
            View = view;
        }

        public UnitLogic Logic { get; }
        public UnitView View { get; }
    }
}