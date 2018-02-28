using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Guajiro.ViewModels
{
    public class MovimientosViewModel : Notifier
    {
        #region Commands
        public RelayCommand BuscarMovimientosCommand { get; set; }
        public RelayCommand RegistrarMovimientoCommand { get; set; }
        public RelayCommand EditarMovimientoCommand { get; set; }
        public RelayCommand BorrarMovimientoCommand { get; set; }
        public RelayCommand CerrarMensajeCommand { get; set; }
        #endregion

        #region Variables
        private DateTime _fechaInicial;
        private DateTime _fechaFinal;
        private string _txtDescripcion;
        private string _txtMensaje;
        private string _idPersona;
        private bool _verMensaje;
        private bool _chkTodos;
        private bool _chkIngreso;
        private bool _chkEgreso;
        private ObservableCollection<vw_lista_movimientos> _listaMovimientos;

        public bd_guajiroEntities GuajiroEF;
        public DateTime FechaInicial { get => _fechaInicial; set { _fechaInicial = value; OnPropertyChanged(); } }
        public DateTime FechaFinal { get => _fechaFinal; set { _fechaFinal = value; OnPropertyChanged(); } }
        public string TxtDescripcion { get => _txtDescripcion; set { _txtDescripcion = value; OnPropertyChanged(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged(); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged(); } }
        public bool ChkTodos { get => _chkTodos; set { _chkTodos = value; OnPropertyChanged(); } }
        public bool ChkIngreso { get => _chkIngreso; set { _chkIngreso = value; OnPropertyChanged(); } }
        public bool ChkEgreso { get => _chkEgreso; set { _chkEgreso = value; OnPropertyChanged(); } }
        public ObservableCollection<vw_lista_movimientos> ListaMovimientos { get => _listaMovimientos; set { _listaMovimientos = value; OnPropertyChanged(); } }
        public string IdPersona { get => _idPersona; set { _idPersona = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public MovimientosViewModel()
        {
            GuajiroEF = new bd_guajiroEntities();
            BuscarMovimientosCommand = new RelayCommand(BuscarMovimientos);
            RegistrarMovimientoCommand = new RelayCommand(RegistrarMovimiento);
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
            string cadSql = "SELECT * FROM vw_lista_movimientos ";
            if (FechaFinal > FechaInicial)
            {
                cadSql += "WHERE fecha BETWEEN '" + FechaInicial.ToString("yyyy-MM-dd") + "' AND '" + FechaFinal.ToString("yyyy-MM-dd") + "' ";
                //var lista = GuajiroEF.vw_lista_movimientos.Where(x => x.fecha >= FechaInicial.Date && x.fecha <= FechaFinal.Date).OrderByDescending(x=>x.fecha).ToList();
                if (string.IsNullOrWhiteSpace(TxtDescripcion) == false)
                    cadSql += "AND descripcion LIKE '%" + TxtDescripcion + "%' ";
                if(ChkIngreso == true)
                    cadSql += "AND idlstipomovimiento = 'e47c55ba-368a-11e7-b904-204747335338' ";
                else if (ChkEgreso==true)
                    cadSql += "AND idlstipomovimiento = 'e47c6e3b-368a-11e7-b904-204747335338' ";
                var lista = GuajiroEF.vw_lista_movimientos.SqlQuery(cadSql).ToList();
                ListaMovimientos = new ObservableCollection<vw_lista_movimientos>(lista);
            }
        }

        private async void RegistrarMovimiento(object parameter)
        {
            var vmDatos = new DatosMovimientoViewModel
            {
                IdPersona = IdPersona
            };
            var vwDatos = new DatosMovimientoView
            {
                DataContext = vmDatos
            };
            var result = await DialogHost.Show(vwDatos, "Movimientos");
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
