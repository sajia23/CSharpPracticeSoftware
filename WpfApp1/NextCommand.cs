using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfApp1
{
    public class NextCommand : ICommand
    {
        private Action next;

        

        public NextCommand(Action nextt)
        {
            next = nextt;
        }

        

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;

            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            next();
        }
    }
}
