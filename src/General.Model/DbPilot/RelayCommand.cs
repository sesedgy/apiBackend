using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GeneralModel
{
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action _execute;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        #endregion

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameters)
        {
            _execute();
        }

        #endregion
    }
}
