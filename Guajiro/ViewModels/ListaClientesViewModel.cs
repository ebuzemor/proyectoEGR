using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;

namespace Guajiro.ViewModels
{
    public class ListaClientesViewModel : Notifier
    {
        #region Commands
        public RelayCommand NuevoClienteCommand { get; set; }
        #endregion

        #region Variables
        public bd_guajiroEntities GuajiroEF;
        #endregion

        #region Constructor
        public ListaClientesViewModel()
        {
            NuevoClienteCommand = new RelayCommand(NuevoCliente);
            GuajiroEF = new bd_guajiroEntities();
        }
        #endregion

        #region Métodos
        private async void NuevoCliente(object parameter)
        {
            var vwDatosCliente = new DatosClienteView { };
            var result = await DialogHost.Show(vwDatosCliente, "ListaClientes");
        }
        #endregion
    }
}
