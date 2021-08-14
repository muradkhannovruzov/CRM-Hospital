using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ReceptionApp.Commands
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        private Action<object> _execute;
        private Func<bool> _canExecute;


        public RelayCommand(Action<object> execute, Func<bool> canExecute = null)
        {
            if (execute == null)
                throw new NullReferenceException();

            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }   
}
