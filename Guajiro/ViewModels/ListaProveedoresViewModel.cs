using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Guajiro.ViewModels
{
    public class ListaProveedoresViewModel : Notifier
    {
        #region Commands
        public RelayCommand NuevoProveedorCommand { get; set; }
        public RelayCommand BuscarProveedorCommand { get; set; }
        public RelayCommand MostrarTodosCommand { get; set; }
        public RelayCommand MostrarUltimosCommand { get; set; }
        public RelayCommand EditarProveedorCommand { get; set; }
        public RelayCommand BorrarProveedorCommand { get; set; }
        #endregion

        #region Variables
        public bd_guajiroEntities GuajiroEF;

        private string _txtBuscar;
        private string _idPersona;
        private ObservableCollection<vw_proveedores_directorio> _listaProveedores;

        public string TxtBuscar { get => _txtBuscar; set { _txtBuscar = value; OnPropertyChanged("TxtBuscar"); } }
        public string IdPersona { get => _idPersona; set { _idPersona = value; OnPropertyChanged("IdPersona"); } }
        public ObservableCollection<vw_proveedores_directorio> ListaProveedores { get => _listaProveedores; set { _listaProveedores = value; OnPropertyChanged("ListaProveedores"); } }
        #endregion

        #region Constructor
        public ListaProveedoresViewModel()
        {
            NuevoProveedorCommand = new RelayCommand(NuevoProveedor);
            BuscarProveedorCommand = new RelayCommand(BuscarProveedor);
            MostrarTodosCommand = new RelayCommand(MostrarTodos);
            MostrarUltimosCommand = new RelayCommand(MostrarUltimos);
            EditarProveedorCommand = new RelayCommand(EditarProveedor);
            BorrarProveedorCommand = new RelayCommand(BorrarProveedor);
            GuajiroEF = new bd_guajiroEntities();
        }
        #endregion

        #region Métodos
        private async void NuevoProveedor(object parameter)
        {
            List<tbl_listadoseldetalle> tiposTelefono = GuajiroEF.tbl_listadoseldetalle.Where(x => x.idlistadoseleccion == "b06d265a-f42a-11e7-83f1-204747335338").ToList();
            List<tbl_estados> listaEstados = GuajiroEF.tbl_estados.ToList();
            var vmDatosProveedor = new DatosProveedorViewModel
            {
                TiposTelefono = new ObservableCollection<tbl_listadoseldetalle>(tiposTelefono),
                ListaEstados = new ObservableCollection<tbl_estados>(listaEstados),
                ListaMunicipios = new ObservableCollection<tbl_municipios>(),
                IdPersona = IdPersona
            };
            var vwDatosProveedor = new DatosProveedorView
            {
                DataContext = vmDatosProveedor
            };
            var result = await DialogHost.Show(vwDatosProveedor, "ListaProveedores");
            //Console.Write(vmDatosProveedor.TxtNPrimario);
        }

        private void BuscarProveedor(object parameter)
        {
            var lista = GuajiroEF.vw_proveedores_directorio.Where(x => x.razon_social.Contains(TxtBuscar)).ToList();
            ListaProveedores = new ObservableCollection<vw_proveedores_directorio>(lista);
        }

        private void MostrarTodos(object parameter)
        {
            var lista = GuajiroEF.vw_proveedores_directorio.ToList();
            ListaProveedores = new ObservableCollection<vw_proveedores_directorio>(lista);
        }

        private void MostrarUltimos(object parameter)
        {
            var lista = GuajiroEF.vw_proveedores_directorio.SqlQuery("SELECT * FROM vw_proveedores_directorio ORDER BY fecha_creacion LIMIT 10").ToList();
            ListaProveedores = new ObservableCollection<vw_proveedores_directorio>(lista);
        }

        private void EditarProveedor(object parameter)
        {

        }

        private void BorrarProveedor(object parameter)
        {

        }
        #endregion
    }
}
