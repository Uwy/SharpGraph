using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpGraph.Extensions;
using SharpGraph.Functions;

namespace SharpGraph.ViewModelUtilities
{
    public delegate void CommandHandler(object parameters);
    public delegate bool CanBeExecutedHandler(object parameters);

    public delegate void EventHandlerSender<T>(T sender);
    public delegate bool CanEventHandlerSender<T>(T sender);

    public class Command : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> action;

        #region ICommand
        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.action(parameter);
        }
        #endregion

        public Command(Action<object> action, Predicate<object> canExecute)
        {
            this.action = action.ThrowIfNull("Command : action null");
            this.canExecute = canExecute.ThrowIfNull("Command : canExecute null");
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public Command(Action<object> action) : this(action, new Predicate<object>(Predicates.ReturnTrue))
        {}
    }
}
