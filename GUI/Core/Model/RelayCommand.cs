using System;
using System.Windows.Input;

namespace GBDPIGUI.Core.Model
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this._Execute = execute;
            this._CanExecute = canExecute;
        }

        private Action<object> _Execute;
        private Func<object, bool> _CanExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => this._CanExecute == null || this._CanExecute(parameter);

        public void Execute(object parameter) => this._Execute(parameter);
    }
}
