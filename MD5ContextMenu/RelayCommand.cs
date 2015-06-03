using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MD5ContextMenu
{
    /// <summary>
    /// Repassa a funcionalidade via delegate para outros objetos
    /// </summary>
    public class RelayCommand : ICommand
    {

        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância de MD5ContextMenu.RelayCommand
        /// </summary>
        /// <param name="execute">Lógica a ser executada</param>
        public RelayCommand(Action<object> execute) : this(execute, null) { }

        /// <summary>
        /// Inicializa uma nova instância de MD5ContextMenu.RelayCommand
        /// </summary>
        /// <param name="execute">Lógica a ser executada</param>
        /// <param name="canExecute">Lógica para permitir execução do comando</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        #endregion
    }
}
