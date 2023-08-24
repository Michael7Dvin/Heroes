using CodeBase.UI.Windows.Base.Window;

namespace CodeBase.UI.Windows.BattleField
{
    public class BattleFieldWindow : BaseWindow
    {
        private readonly BattleFieldWindowView _view;
        private readonly BattleFieldWindowLogic _logic;

        public BattleFieldWindow(BattleFieldWindowView view, BattleFieldWindowLogic logic) : base(view)
        {
            _view = view;
            _logic = logic;
        }

        public override WindowID Type => WindowID.BattleField;
        protected override void OnOpened()
        {
            _view.EndTurnButtonClicked += _logic.EndTurn;
        }

        protected override void OnClosed()
        {
            _view.EndTurnButtonClicked -= _logic.EndTurn;
        }
    }
}