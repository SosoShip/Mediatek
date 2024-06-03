using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SVE.Mediatek.ViewModel.ViewModels
{
    public class CommandHandler : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public required Action<object?> CommandExecute { get; set; }
        public Func<object?, bool>? CommandCanExecute { get; set; }

        public bool CanExecute(object? parameter)
        {
            return CommandCanExecute == null ? true : CommandCanExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            CommandExecute(parameter);
        }
    }
}
