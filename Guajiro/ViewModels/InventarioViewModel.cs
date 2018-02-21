﻿using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Guajiro.ViewModels
{
    public class InventarioViewModel : Notifier
    {
        #region Commands
        public RelayCommand NuevoProductoCommand { get; set; }
        public RelayCommand BuscarProductoCommand { get; set; }
        public RelayCommand MostrarTodosCommand { get; set; }
        public RelayCommand MostrarUltimosCommand { get; set; }
        public RelayCommand EditarProductoCommand { get; set; }
        public RelayCommand BorrarProductoCommand { get; set; }
        #endregion

        #region Variables
        public bd_guajiroEntities GuajiroEF;
        private string _txtBuscar;
        private string _idPersona;
        private ObservableCollection<vw_lista_productos> _listaProductos;

        public string TxtBuscar { get => _txtBuscar; set { _txtBuscar = value; OnPropertyChanged(); } }
        public string IdPersona { get => _idPersona; set { _idPersona = value; OnPropertyChanged(); } }
        public ObservableCollection<vw_lista_productos> ListaProductos { get => _listaProductos; set { _listaProductos = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public InventarioViewModel()
        {
            NuevoProductoCommand = new RelayCommand(NuevoProducto);
            BuscarProductoCommand = new RelayCommand(BuscarProducto);
            MostrarTodosCommand = new RelayCommand(MostrarTodos);
            MostrarUltimosCommand = new RelayCommand(MostrarUltimos);
            EditarProductoCommand = new RelayCommand(EditarProducto);
            BorrarProductoCommand = new RelayCommand(BorrarProducto);
            GuajiroEF = new bd_guajiroEntities();
        }
        #endregion

        #region Métodos
        private async void NuevoProducto(object parameter)
        {
            var vmDatosProducto = new DatosProductoViewModel
            {
                IdPersona = IdPersona
            };
            var vwDatosProducto = new DatosProductoView
            {
                DataContext = vmDatosProducto
            };
            var result = await DialogHost.Show(vwDatosProducto, "Inventario");
        }

        private void BuscarProducto(object parameter)
        {
            var lista = GuajiroEF.vw_lista_productos.Where(x => x.descripcion.Contains(TxtBuscar)).ToList();
            ListaProductos = new ObservableCollection<vw_lista_productos>(lista);
        }

        private void MostrarTodos(object parameter)
        {
            var lista = GuajiroEF.vw_lista_productos.ToList();
            ListaProductos = new ObservableCollection<vw_lista_productos>(lista);
        }

        private void MostrarUltimos(object parameter)
        {
            var lista = GuajiroEF.vw_lista_productos.SqlQuery("SELECT * FROM vw_lista_productos ORDER BY fecha_creacion LIMIT 10").ToList();
            ListaProductos = new ObservableCollection<vw_lista_productos>(lista);
        }

        private async void EditarProducto(object parameter)
        {
            string idItem = parameter as string;
            var prod = GuajiroEF.tbl_items.SingleOrDefault(x => x.iditem == idItem);
            var unid = GuajiroEF.tbl_listadoseldetalle.SingleOrDefault(x => x.idlsselecciondetalle == prod.idlstipoitem);
            var car = GuajiroEF.tbl_caracteristicasitem.SingleOrDefault(x => x.iditem == prod.iditem);
            var lista = GuajiroEF.tbl_listadoseldetalle.Where(x => x.idlistadoseleccion == "5fd493ef-3688-11e7-b904-204747335338"
                                                                || x.idlistadoseleccion == "434bf20e-3688-11e7-b904-204747335338").ToList();
            var vmDatos = new DatosProductoViewModel
            {
                IdPersona = IdPersona,
                TxtDescripcion = prod.descripcion,
                TxtExistencias = Convert.ToDecimal(prod.existencia),
                ChkInventariable = Convert.ToBoolean(prod.inventariable),
                TxtPrecio = Convert.ToDecimal(car.valor),
                ListaUnidades=new ObservableCollection<tbl_listadoseldetalle>(lista),
                Unidad = unid
            };
            var vwDatos = new DatosProductoView
            {
                DataContext = vmDatos
            };
            var result = await DialogHost.Show(vwDatos, "Inventario");
        }

        private void BorrarProducto(object parameter)
        {

        }
        #endregion
    }
}
