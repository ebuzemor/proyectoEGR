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
    public class DatosMovimientoViewModel : Notifier
    {
        #region Commands
        public RelayCommand GuardarMovimientoCommand { get; set; }
        public RelayCommand CerrarMensajeCommand { get; set; }
        #endregion

        #region Variables
        private DateTime _fechaMov;
        private string _txtDescripcion;
        private string _txtMensaje;
        private string _idPersona;
        private bool _verMensaje;
        private double _txtMonto;
        private ObservableCollection<tbl_listadoseldetalle> _listaTiposMov;
        private tbl_listadoseldetalle _tipoMov;

        public bd_guajiroEntities GuajiroEF;
        public DateTime FechaMov { get => _fechaMov; set { _fechaMov = value; OnPropertyChanged(); } }
        public string TxtDescripcion { get => _txtDescripcion; set { _txtDescripcion = value; OnPropertyChanged(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged(); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged(); } }
        public double TxtMonto { get => _txtMonto; set { _txtMonto = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_listadoseldetalle> ListaTiposMov { get => _listaTiposMov; set { _listaTiposMov = value; OnPropertyChanged(); } }
        public tbl_listadoseldetalle TipoMov { get => _tipoMov; set { _tipoMov = value; OnPropertyChanged(); } }
        public string IdPersona { get => _idPersona; set { _idPersona = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public DatosMovimientoViewModel()
        {
            GuardarMovimientoCommand = new RelayCommand(GuardarMovimiento);
            CerrarMensajeCommand = new RelayCommand(CerrarMensaje);
            FechaMov = DateTime.Now;
            GuajiroEF = new bd_guajiroEntities();
            var lista = GuajiroEF.tbl_listadoseldetalle.ToList();
            ListaTiposMov = new ObservableCollection<tbl_listadoseldetalle>(lista);
            
        }
        #endregion

        #region Métodos
        private void GuardarMovimiento(object parameter)
        {

        }

        private void CerrarMensaje(object parameter) => VerMensaje = false;
        #endregion
    }
}
