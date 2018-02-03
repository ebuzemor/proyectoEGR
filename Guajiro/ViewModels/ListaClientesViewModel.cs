using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        private string _creaUsuario;
        private ObservableCollection<vw_clientes_directorio> _listaClientes;

        public string TxtBuscar { get => _txtBuscar; set { _txtBuscar = value; OnPropertyChanged("TxtBuscar"); } }
        public string CreaUsuario { get => _creaUsuario; set { _creaUsuario = value; OnPropertyChanged("CreaUsuario"); } }
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
            List<tbl_estados> listaEstados = GuajiroEF.tbl_estados.ToList();
            var vmDatosCliente = new DatosClienteViewModel
            {
                TiposTelefono = new ObservableCollection<tbl_listadoseldetalle>(tiposTelefono),
                ListaEstados = new ObservableCollection<tbl_estados>(listaEstados),
                ListaMunicipios = new ObservableCollection<tbl_municipios>(),
                CreaUsuario = CreaUsuario
            };
            var vwDatosCliente = new DatosClienteView
            {
                DataContext = vmDatosCliente
            };
            var result = await DialogHost.Show(vwDatosCliente, "ListaClientes");
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

        private async void EditarCliente(object parameter)
        {
            string idpersona = parameter as string;
            tbl_personas cliente = GuajiroEF.tbl_personas.SingleOrDefault(x => x.idpersona == idpersona);
            List<vw_lista_telefonos> ltaTel = GuajiroEF.vw_lista_telefonos.Where(x => x.idpersona == idpersona).ToList();
            List<tbl_direcciones> ltaDir = GuajiroEF.tbl_direcciones.Where(x => x.idpersona == idpersona).ToList();
            List<tbl_estados> ltaEdo = GuajiroEF.tbl_estados.ToList();
            List<tbl_listadoseldetalle> tiposTelefono = GuajiroEF.tbl_listadoseldetalle.Where(x => x.idlistadoseleccion == "b06d265a-f42a-11e7-83f1-204747335338").ToList();
            var vmDatos = new DatosClienteViewModel
            {
                IdPersona = cliente.idpersona,
                ChkMoral = (cliente.idlstipocontribuyente == "e0e8f331-fe83-11e7-83f1-204747335338") ? true : false,
                ChkFisica = (cliente.idlstipocontribuyente == "e0e8f331-fe83-11e7-83f1-204747335338") ? false : true,
                TxtNPrimario = (cliente.idlstipocontribuyente == "e0e8f331-fe83-11e7-83f1-204747335338") ? null : cliente.nprimario,
                TxtNSecundario = (cliente.idlstipocontribuyente == "e0e8f331-fe83-11e7-83f1-204747335338") ? null : cliente.nsecundario,
                TxtPaterno = (cliente.idlstipocontribuyente == "e0e8f331-fe83-11e7-83f1-204747335338") ? null : cliente.paterno,
                TxtMaterno = (cliente.idlstipocontribuyente == "e0e8f331-fe83-11e7-83f1-204747335338") ? null : cliente.materno,
                TxtRazonSocial = (cliente.idlstipocontribuyente == "e0e8f331-fe83-11e7-83f1-204747335338") ? cliente.razon_social : null,
                TxtRFC = cliente.rfc,
                TxtEmail = cliente.email,
                ListaTelefonos = new ObservableCollection<vw_lista_telefonos>(ltaTel),
                ListaDirecciones = new ObservableCollection<tbl_direcciones>(ltaDir),
                ListaEstados = new ObservableCollection<tbl_estados>(ltaEdo),
                ListaMunicipios = new ObservableCollection<tbl_municipios>(),
                TiposTelefono = new ObservableCollection<tbl_listadoseldetalle>(tiposTelefono)
            };
            var vwDatos = new DatosClienteView
            {
                DataContext = vmDatos
            };
            var result = await DialogHost.Show(vwDatos, "ListaClientes");
        }

        private void BorrarCliente(object parameter)
        {

        }
        #endregion
    }
}
