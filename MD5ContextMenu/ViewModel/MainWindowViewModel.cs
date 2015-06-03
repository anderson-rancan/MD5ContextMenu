using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MD5ContextMenu.ViewModel
{
    internal class MainWindowViewModel : WorkspaceViewModel
    {

        RelayCommand clearCommand;
        RelayCommand exportCommand;

        /// <summary>
        /// Retorna o comando que limpa a coleção de arquivos
        /// </summary>
        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new RelayCommand(
                        param => this.Clear(),
                        param => this.CanClear
                        );
                }
                return clearCommand;
            }
        }

        /// <summary>
        /// Retorna o comando que exporta os hashs MD5
        /// </summary>
        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                {
                    exportCommand = new RelayCommand(
                        param => this.Export(),
                        param => this.CanExport
                        );
                }
                return exportCommand;
            }
        }

        /// <summary>
        /// Limpa a coleção de arquivos
        /// </summary>
        public void Clear()
        {
            using (var task = new Microsoft.WindowsAPICodePack.Dialogs.TaskDialog())
            {
                task.Text = "Funca!";
                task.StandardButtons = Microsoft.WindowsAPICodePack.Dialogs.TaskDialogStandardButtons.Ok;
                task.Icon = Microsoft.WindowsAPICodePack.Dialogs.TaskDialogStandardIcon.Information;
                task.Show();
            }
        }

        /// <summary>
        /// Retorna condição de permissão para limpeza da coleção
        /// </summary>
        /// <remarks>
        /// Retorna sempre <c>true</c>
        /// </remarks>
        bool CanClear
        {
            get { return true; }
        }

        /// <summary>
        /// Retorna condição de permissão para exportação do resultado de hashs
        /// </summary>
        bool CanExport
        {
            get { return true; /*throw new NotImplementedException("MD5ContextMenu.ViewModel.MainWindowViewModel.CanExport");*/ }
        }

        private object Export()
        {
            throw new NotImplementedException("MD5ContextMenu.ViewModel.MainWindowViewModel.Export");
        }

    }
}
