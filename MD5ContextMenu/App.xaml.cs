using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using MD5ContextMenu.ViewModel;

namespace MD5ContextMenu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (new EnableThemingInScope(true))
            {
                MainWindow window = new MainWindow();
                MainWindowViewModel viewModel = new MainWindowViewModel();

                EventHandler handler = null;
                handler = delegate
                {
                    viewModel.RequestClose -= handler;
                    window.Close();
                };
                viewModel.RequestClose += handler;

                window.DataContext = viewModel;
                window.Show();
            }
        }
    }
}
