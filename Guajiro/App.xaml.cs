using System.Windows;
using Guajiro.Common;
using Guajiro.Views;
using Guajiro.ViewModels;
using Guajiro.Models;

namespace Guajiro
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainView main = new MainView();
            //MainViewModel mvm = new MainViewModel();
            Navigator.NavigationService = main.Navegador.NavigationService;
            main.Show();

            LoginViewModel lvm = new LoginViewModel
            {
                guajiroEF = new bd_guajiroEntities()
            };

            LoginView login = new LoginView
            {
                DataContext = lvm
            };
            Navigator.NavigationService.Navigate(login);
        }
    }
}
