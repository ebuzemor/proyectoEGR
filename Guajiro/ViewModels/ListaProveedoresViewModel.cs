﻿using Guajiro.Common;
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
        public RelayCommand CerrarMensajeCommand { get; set; }
        #endregion

        #region Variables
        public bd_guajiroEntities GuajiroEF;

        private string _txtBuscar;
        private string _creaUsuario;
        private string _txtMensaje;
        private bool _verMensaje;
        private ObservableCollection<vw_proveedores_directorio> _listaProveedores;

        public string TxtBuscar { get => _txtBuscar; set { _txtBuscar = value; OnPropertyChanged(); } }
        public string CreaUsuario { get => _creaUsuario; set { _creaUsuario = value; OnPropertyChanged(); } }
        public ObservableCollection<vw_proveedores_directorio> ListaProveedores { get => _listaProveedores; set { _listaProveedores = value; OnPropertyChanged(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged(); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged(); } }
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
            CerrarMensajeCommand = new RelayCommand(CerrarMensaje);
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
                CreaUsuario = CreaUsuario
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

        private async void EditarProveedor(object parameter)
        {
            string idpersona = parameter as string;
            tbl_personas cliente = GuajiroEF.tbl_personas.SingleOrDefault(x => x.idpersona == idpersona);
            List<vw_lista_telefonos> ltaTel = GuajiroEF.vw_lista_telefonos.Where(x => x.idpersona == idpersona).ToList();
            List<tbl_direcciones> ltaDir = GuajiroEF.tbl_direcciones.Where(x => x.idpersona == idpersona).ToList();
            List<tbl_estados> ltaEdo = GuajiroEF.tbl_estados.ToList();
            List<tbl_listadoseldetalle> tiposTelefono = GuajiroEF.tbl_listadoseldetalle.Where(x => x.idlistadoseleccion == "b06d265a-f42a-11e7-83f1-204747335338").ToList();
            var vmDatos = new DatosProveedorViewModel
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
            var vwDatos = new DatosProveedorView
            {
                DataContext = vmDatos
            };
            var result = await DialogHost.Show(vwDatos, "ListaProveedores");
            GuajiroEF = new bd_guajiroEntities();
            var lista = GuajiroEF.vw_proveedores_directorio.ToList();
            ListaProveedores = new ObservableCollection<vw_proveedores_directorio>(lista);
        }

        private async void BorrarProveedor(object parameter)
        {
            string idProveedor = parameter as string;
            var vmMensaje = new MensajeViewModel
            {
                TituloMensaje = "Advertencia",
                CuerpoMensaje = "¿Deseas borrar la información del Proveedor seleccionado?",
                MostrarCancelar = true,
                TxtAceptar = "SI",
                TxtCancelar = "NO"
            };
            var vwMensaje = new MensajeView
            {
                DataContext = vmMensaje
            };
            var result = await DialogHost.Show(vwMensaje, "ListaProveedores");
            if (result.Equals("OK") == true)
            {
                tbl_personas prov = GuajiroEF.tbl_personas.SingleOrDefault(x => x.idpersona == idProveedor);
                using (var bd = new bd_guajiroEntities())
                {
                    bd.tbl_telefonos.RemoveRange(bd.tbl_telefonos.Where(x => x.idpersona == idProveedor));
                    bd.tbl_direcciones.RemoveRange(bd.tbl_direcciones.Where(x => x.idpersona == idProveedor));
                    bd.Entry(prov).State = System.Data.Entity.EntityState.Deleted;
                    int c = bd.SaveChanges();
                    if (c > 0)
                    {
                        TxtMensaje = "Los datos del Proveedor " + prov.razon_social + " fueron borrados correctamente";
                        VerMensaje = true;
                        var lista = GuajiroEF.vw_proveedores_directorio.ToList();
                        ListaProveedores = new ObservableCollection<vw_proveedores_directorio>(lista);
                    }
                }
            }
        }

        private void CerrarMensaje(object parameter) => VerMensaje = false;
        #endregion
    }
}
