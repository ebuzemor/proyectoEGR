﻿using Guajiro.Common;
using Guajiro.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Guajiro.ViewModels
{
    public class DatosProductoViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public RelayCommand GuardarProductoCommand { get; set; }
        public RelayCommand CerrarMensajeCommand { get; set; }
        #endregion

        #region Variables
        private string _txtDescripcion;
        private decimal _txtExistencias;
        private decimal _txtPrecio;
        private string _txtMensaje;
        private string _idPersona;
        private bool _verMensaje;
        private bool _chkInventariable;
        private tbl_listadoseldetalle _unidad;
        private ObservableCollection<tbl_listadoseldetalle> _listaUnidades;

        public bd_guajiroEntities GuajiroEF;
        public string TxtDescripcion { get => _txtDescripcion; set { _txtDescripcion = value; OnPropertyChanged(); } }
        public decimal TxtExistencias { get => _txtExistencias; set { _txtExistencias = value; OnPropertyChanged(); } }
        public decimal TxtPrecio { get => _txtPrecio; set { _txtPrecio = value; OnPropertyChanged(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged(); } }
        public string IdPersona { get => _idPersona; set { _idPersona = value; OnPropertyChanged(); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged(); } }
        public bool ChkInventariable { get => _chkInventariable; set { _chkInventariable = value; OnPropertyChanged(); FiltrarUnidades(); } }
        public tbl_listadoseldetalle Unidad { get => _unidad; set { _unidad = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_listadoseldetalle> ListaUnidades { get => _listaUnidades; set { _listaUnidades = value; OnPropertyChanged(); } }        
        #endregion

        #region Constructor
        public DatosProductoViewModel()
        {
            GuardarProductoCommand = new RelayCommand(GuardarProducto);
            CerrarMensajeCommand = new RelayCommand(CerrarMensaje);
            GuajiroEF = new bd_guajiroEntities();
            FiltrarUnidades();
        }
        #endregion

        #region Métodos
        private bool ValidarDatos()
        {
            bool check = string.IsNullOrWhiteSpace(TxtDescripcion);
            if (check != true)
                check = (TxtPrecio > 0) ? false : true;
            if (check != true)
                check = (Unidad != null) ? false : true;
            return check;
        }

        private void GuardarProducto(object parameter)
        {
            bool ban = ValidarDatos();
            if(ban == false)
            {
                int guardados = 0;
                using(var bd = new bd_guajiroEntities())
                {
                    tbl_items item = new tbl_items
                    {
                        iditem = Convert.ToString(Guid.NewGuid()),
                        descripcion = TxtDescripcion,
                        existencia = TxtExistencias,
                        idlstipoitem = Unidad.idlsselecciondetalle,
                        inventariable = ChkInventariable,
                        crea_usuario = IdPersona,
                        fecha_creacion = DateTime.Now
                    };
                    bd.tbl_items.Add(item);
                    tbl_caracteristicasitem car = new tbl_caracteristicasitem
                    {
                        idcaracteristica = Convert.ToString(Guid.NewGuid()),
                        iditem = item.iditem,
                        idlsunidadmedida = Unidad.idlsselecciondetalle,
                        idlstipocaracteristica = "e47c6405-368a-11e7-b904-204747335338", //Precio
                        valor = TxtPrecio,
                        crea_usuario = IdPersona,
                        fecha_creacion = DateTime.Now
                    };
                    bd.tbl_caracteristicasitem.Add(car);
                    guardados = bd.SaveChanges();
                    if (guardados > 0)
                    {
                        TxtMensaje = "Los datos del Producto han sido guardados correctamente";
                        VerMensaje = true;
                        LimpiarPantalla();
                    }
                }
            }
            else
            {
                TxtMensaje = "Debes ingresar datos en los campos obligatorios";
                VerMensaje = true;
            }
        }

        private void LimpiarPantalla()
        {
            TxtDescripcion = "";
            TxtExistencias = 0;
            TxtPrecio = 0;
            ChkInventariable = false;
            Unidad = null;
            FiltrarUnidades();
        }

        private void FiltrarUnidades()
        {
            List<tbl_listadoseldetalle> lista = new List<tbl_listadoseldetalle>();
            if (ChkInventariable == true)
                lista = GuajiroEF.tbl_listadoseldetalle.Where(x => x.idlistadoseleccion == "5fd493ef-3688-11e7-b904-204747335338").ToList(); //Tipo: Unidad
            else
                lista = GuajiroEF.tbl_listadoseldetalle.Where(x => x.idlistadoseleccion == "434bf20e-3688-11e7-b904-204747335338").ToList(); //Tipo: Característica
            ListaUnidades = new ObservableCollection<tbl_listadoseldetalle>(lista);
        }

        private void CerrarMensaje(object parameter) => VerMensaje = false;
        #endregion
    }
}
