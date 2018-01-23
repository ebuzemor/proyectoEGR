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
    public class MovimientosViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public RelayCommand BuscarMovimientosCommand { get; set; }
        public RelayCommand RegistrarIngresosCommand { get; set; }
        public RelayCommand RegistrarGastosCommand { get; set; }
        public RelayCommand EditarMovimientoCommand { get; set; }
        public RelayCommand BorrarMovimientoCommand { get; set; }
        public RelayCommand CerrarMensajeCommand { get; set; }
        #endregion

        #region Variables
        private DateTime _fechaInicial;
        private DateTime _fechaFinal;
        private string _txtDescripcion;
        private string _txtMensaje;
        private bool _verMensaje;
        private bool _chkTodos;
        private bool _chkIngreso;
        private bool _chkEgreso;
        private ObservableCollection<tbl_movimientos> _listaMovimientos;

        public bd_guajiroEntities GuajiroEF;
        public DateTime FechaInicial { get => _fechaInicial; set { _fechaInicial = value; OnPropertyChanged(); } }
        public DateTime FechaFinal { get => _fechaFinal; set { _fechaFinal = value; OnPropertyChanged(); } }
        public string TxtDescripcion { get => _txtDescripcion; set { _txtDescripcion = value; OnPropertyChanged(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged(); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged(); } }
        public bool ChkTodos { get => _chkTodos; set { _chkTodos = value; OnPropertyChanged(); } }
        public bool ChkIngreso { get => _chkIngreso; set { _chkIngreso = value; OnPropertyChanged(); } }
        public bool ChkEgreso { get => _chkEgreso; set { _chkEgreso = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_movimientos> ListaMovimientos { get => _listaMovimientos; set { _listaMovimientos = value; OnPropertyChanged(); } }        
        #endregion

        #region Constructor
        public MovimientosViewModel()
        {
            GuajiroEF = new bd_guajiroEntities();
            BuscarMovimientosCommand = new RelayCommand(BuscarMovimientos);
            RegistrarIngresosCommand = new RelayCommand(RegistrarIngresos);
            RegistrarGastosCommand = new RelayCommand(RegistrarGastos);
            EditarMovimientoCommand = new RelayCommand(EditarMovimiento);
            BorrarMovimientoCommand = new RelayCommand(BorrarMovimiento);
            CerrarMensajeCommand = new RelayCommand(CerrarMensaje);
            DateTime hoy = DateTime.Now;
            FechaInicial = new DateTime(hoy.Year, hoy.Month, 1);
            FechaFinal = FechaInicial.AddMonths(1).AddDays(-1);
            ChkTodos = true;
        }
        #endregion

        #region Métodos
        private void BuscarMovimientos(object parameter)
        {
            if (FechaFinal > FechaInicial)
            {
                var lista = GuajiroEF.tbl_movimientos.SqlQuery("SELECT * FROM tbl_movimientos WHERE fecha BETWEEN " + FechaInicial.Date + " AND " + FechaFinal.Date).ToList();
                ListaMovimientos = new ObservableCollection<tbl_movimientos>(lista);
            }
        }

        private async void RegistrarIngresos(object parameter)
        {

        }

        private async void RegistrarGastos(object parameter)
        {

        }

        private void EditarMovimiento(object parameter)
        {

        }

        private void BorrarMovimiento(object parameter)
        {

        }

        private void CerrarMensaje(object parameter) => VerMensaje = false;

        private void FiltrarMovimientos()
        {

        }
        #endregion
    }
}
