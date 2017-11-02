using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Guajiro.ViewModels;

namespace Guajiro.Views
{
    /// <summary>
    /// Lógica de interacción para PrincipalView.xaml
    /// </summary>
    public partial class PrincipalView : UserControl
    {
        public PrincipalView()
        {
            InitializeComponent();
            DataContext = new PrincipalViewModel();
        }

        private void ListaOpciones_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //mientras está abierto el menú, esto ayudará con los scrollbars del menu
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            BotonMenuToggle.IsChecked = true;
        }
    }
}
