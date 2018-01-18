using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using System.Collections.ObjectModel;

namespace Guajiro.ViewModels
{
    public class ListaClientesViewModel : Notifier
    {
        #region Commands
        public RelayCommand NuevoClienteCommand { get; set; }
        public RelayCommand BuscarClienteCommand { get; set; }
        public RelayCommand MostrarTodosCommand { get; set; }
        public RelayCommand MostrarUltimosCommand { get; set; }
        public RelayCommand EditarClienteCommand { get; set; }
        public RelayCommand BorrarClienteCommand { get; set; }        
        #endregion

        #region Variables
        public bd_guajiroEntities GuajiroEF;

        private string _txtBuscar;
        private ObservableCollection<vw_clientes_directorio> _listaClientes;

        public string TxtBuscar { get => _txtBuscar; set { _txtBuscar = value; OnPropertyChanged("TxtBuscar"); } }
        public ObservableCollection<vw_clientes_directorio> ListaClientes { get => _listaClientes; set { _listaClientes = value; OnPropertyChanged("ListaClientes"); } }
        #endregion

        #region Constructor
        public ListaClientesViewModel()
        {
            NuevoClienteCommand = new RelayCommand(NuevoCliente);
            BuscarClienteCommand = new RelayCommand(BuscarCliente);
            MostrarTodosCommand = new RelayCommand(MostrarTodos);
            MostrarUltimosCommand = new RelayCommand(MostrarUltimos);
            EditarClienteCommand = new RelayCommand(EditarCliente);
            BorrarClienteCommand = new RelayCommand(BorrarCliente);
            GuajiroEF = new bd_guajiroEntities();
        }
        #endregion

        #region Métodos
        private async void NuevoCliente(object parameter)
        {
            List<tbl_listadoseldetalle> tiposTelefono = GuajiroEF.tbl_listadoseldetalle.Where(x => x.idlistadoseleccion == "b06d265a-f42a-11e7-83f1-204747335338").ToList();
            List<tbl_municipios> listaMunicipios = GuajiroEF.tbl_municipios.ToList();
            List<tbl_estados> listaEstados = GuajiroEF.tbl_estados.ToList();
            var vmDatosCliente = new DatosClienteViewModel
            {
                TiposTelefono = new ObservableCollection<tbl_listadoseldetalle>(tiposTelefono),
                ListaEstados = new ObservableCollection<tbl_estados>(listaEstados),
                ListaMunicipios = new ObservableCollection<tbl_municipios>(listaMunicipios)
            };
            var vwDatosCliente = new DatosClienteView
            {
                DataContext = vmDatosCliente
            };
            var result = await DialogHost.Show(vwDatosCliente, "ListaClientes");
            Console.Write(vmDatosCliente.TxtNPrimario);
        }

        private void BuscarCliente(object parameter)
        {
            var lista = GuajiroEF.vw_clientes_directorio.Where(x => x.razon_social.Contains(TxtBuscar)).ToList();
            ListaClientes = new ObservableCollection<vw_clientes_directorio>(lista);
        }

        private void MostrarTodos(object parameter)
        {
            var lista = GuajiroEF.vw_clientes_directorio.ToList();
            ListaClientes = new ObservableCollection<vw_clientes_directorio>(lista);
        }

        private void MostrarUltimos(object parameter)
        {
            var lista = GuajiroEF.vw_clientes_directorio.SqlQuery("SELECT * FROM vw_clientes_directorio ORDER BY fecha_creacion LIMIT 10").ToList();
            ListaClientes = new ObservableCollection<vw_clientes_directorio>(lista);
        }

        private void EditarCliente(object parameter)
        {

        }

        private void BorrarCliente(object parameter)
        {

        }
        #endregion
    }
}
