using CodeBase.UI.Windows.Base.Window;

namespace CodeBase.UI.Windows.ResultsWindow
{
    public class ResultsWindow : BaseWindow
    {
        private readonly ResultsWindowView _view;
        private readonly ResultsWindowLogic _logic;

        public ResultsWindow(WindowID id, ResultsWindowView view, ResultsWindowLogic logic) : base(id, view)
        {
            _view = view;
            _logic = logic;
        }

        protected override void OnOpened()
        {
            _view.RestartButtonClicked += _logic.Restart;
        }

        protected override void OnClosed()
        {
            _view.RestartButtonClicked -= _logic.Restart;
        }
    }
}