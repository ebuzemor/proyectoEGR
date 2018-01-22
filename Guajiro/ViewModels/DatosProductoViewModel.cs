using Guajiro.Common;
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
        #endregion

        #region Variables
        private string _txtDescripcion;
        private string _txtExistencias;
        private string _txtPrecio;
        private string _unidad;
        private bool _chkInventariable;
        private ObservableCollection<tbl_listadoseldetalle> _listaUnidades;

        public bd_guajiroEntities GuajiroEF;
        public string TxtDescripcion { get => _txtDescripcion; set { _txtDescripcion = value; OnPropertyChanged(); } }
        public string TxtExistencias { get => _txtExistencias; set { _txtExistencias = value; OnPropertyChanged(); } }
        public string TxtPrecio { get => _txtPrecio; set { _txtPrecio = value; OnPropertyChanged(); } }
        public string Unidad { get => _unidad; set { _unidad = value; OnPropertyChanged(); } }
        public bool ChkInventariable { get => _chkInventariable; set { _chkInventariable = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_listadoseldetalle> ListaUnidades { get => _listaUnidades; set { _listaUnidades = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public DatosProductoViewModel()
        {
            GuardarProductoCommand = new RelayCommand(GuardarProducto);
            GuajiroEF = new bd_guajiroEntities();
        }
        #endregion

        #region Métodos
        private void ValidarDatos()
        {

        }

        private void GuardarProducto(object parameter)
        {

        }
        #endregion
    }
}
