using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MD5ContextMenu.ViewModel
{
    /// <summary>
    /// Este ViewModel remove da UI quando o comando Close é executado
    /// </summary>
    internal abstract class WorkspaceViewModel : ViewModelBase
    {

        /// <summary>
        /// Evento liberado quando o workspace deve ser removido da janela
        /// </summary>
        public event EventHandler RequestClose;

        RelayCommand closeCommand;

        #region Constructors

        protected WorkspaceViewModel()
        {
        }

        #endregion

        /// <summary>
        /// Retorna o comando que remove este workspace da janela
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (this.closeCommand == null)
                    this.closeCommand = new RelayCommand(param => this.OnRequestClose());

                return this.closeCommand;
            }
        }

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

    }
}
