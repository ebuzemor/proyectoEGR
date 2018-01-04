using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MaterialDesignThemes.Wpf;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Guajiro.ViewModels
{
    public class ComandasViewModel : Notifier
    {
        #region Commands
        public RelayCommand InicioCommand { get; set; }
        public RelayCommand AnteriorCommand { get; set; }
        public RelayCommand SiguienteCommand { get; set; }
        public RelayCommand FinalCommand { get; set; }
        public RelayCommand BuscarComandasCommand { get; set; }
        public RelayCommand MostrarDetallesCommand { get; set; }
        #endregion

        #region Variables
        private int _itemsPorPag;
        private int _pagsTotales;
        private int _indicePagActual;
        private int _pagActual;
        private Boolean _activoInicio;
        private Boolean _activoAnterior;
        private Boolean _activoSiguiente;
        private Boolean _activoFinal;
        private CollectionViewSource _cvsComandas;
        private ObservableCollection<vw_lista_comandas> _listaComandas;
        private ObservableCollection<tbl_detallescomanda> _listaDetalles;
        private DateTime _fechaInicial;
        private DateTime _fechaFinal;
        private String _txtNumInicio;
        private String _txtNumFinal;
        private String _txtCliente;
        private Boolean _esParaLlevar;

        public int ItemsPorPag { get => _itemsPorPag; set { _itemsPorPag = value; OnPropertyChanged("ItemsPorPag"); }}
        public int PagsTotales { get => _pagsTotales; set { _pagsTotales = value; OnPropertyChanged("PagsTotales"); } }
        public int IndicePagActual { get => _indicePagActual; set { _indicePagActual = value; OnPropertyChanged("IndicePagActual"); } }
        public int PagActual { get => _pagActual; set { _pagActual = value; OnPropertyChanged("PagActual"); } }
        public bool ActivoInicio { get => _activoInicio; set { _activoInicio = value; OnPropertyChanged("ActivoInicio"); } }
        public bool ActivoAnterior { get => _activoAnterior; set { _activoAnterior = value; OnPropertyChanged("ActivoAnterior"); } }
        public bool ActivoSiguiente { get => _activoSiguiente; set { _activoSiguiente = value; OnPropertyChanged("ActivoSiguiente"); } }
        public bool ActivoFinal { get => _activoFinal; set { _activoFinal = value; OnPropertyChanged("ActivoFinal"); } }
        public CollectionViewSource CvsComandas { get => _cvsComandas; set { _cvsComandas = value; OnPropertyChanged("CvsComandas"); } }
        public ObservableCollection<vw_lista_comandas> ListaComandas { get => _listaComandas; set { _listaComandas = value; OnPropertyChanged("ListaComandas"); } }
        public DateTime FechaInicial { get => _fechaInicial; set { _fechaInicial = value; OnPropertyChanged("FechaInicial"); } }
        public DateTime FechaFinal { get => _fechaFinal; set { _fechaFinal = value; OnPropertyChanged("FechaFinal"); } }
        public string TxtNumInicio { get => _txtNumInicio; set { _txtNumInicio = value; OnPropertyChanged("TxtNumInicio"); } }
        public string TxtNumFinal { get => _txtNumFinal; set { _txtNumFinal = value; OnPropertyChanged("TxtNumFinal"); } }
        public string TxtCliente { get => _txtCliente; set { _txtCliente = value; OnPropertyChanged("TxtCliente"); } }
        public bool EsParaLlevar { get => _esParaLlevar; set { _esParaLlevar = value; OnPropertyChanged("EsParaLlevar"); filtarParaLlevar(); } }
        public ObservableCollection<tbl_detallescomanda> ListaDetalles { get => _listaDetalles; set { _listaDetalles = value; OnPropertyChanged("ListaDetalles"); } }

        public bd_guajiroEntities GuajiroEF;
        #endregion

        #region Constructor
        public ComandasViewModel()
        {
            GuajiroEF = new bd_guajiroEntities();
            BuscarComandasCommand = new RelayCommand(BuscarComandas);
            MostrarDetallesCommand = new RelayCommand(MostrarDetalles);
            DateTime hoy = DateTime.Now;
            FechaInicial = new DateTime(hoy.Year, hoy.Month, 1);
            FechaFinal = FechaInicial.AddMonths(1).AddDays(-1);
            IndicePagActual = 0;
            ItemsPorPag = 10;
            TxtNumInicio = string.Empty;
            TxtNumFinal = string.Empty;
        }
        #endregion

        #region Metodos
        private async void BuscarComandas(object parameter)
        {
            List<vw_lista_comandas> lista = new List<vw_lista_comandas>();
            int numInicio = string.IsNullOrEmpty(TxtNumInicio) ? 0 : Convert.ToInt32(TxtNumInicio);
            int numFinal = string.IsNullOrEmpty(TxtNumFinal) ? 0 : Convert.ToInt32(TxtNumFinal);
            if ((FechaFinal >= FechaInicial) && (numFinal >= numInicio) && (numFinal > 0 && numInicio > 0) && (string.IsNullOrEmpty(TxtCliente) == false))
            {
                lista = GuajiroEF.vw_lista_comandas.Where(x =>
                            (x.fecha >= FechaInicial && x.fecha <= FechaFinal)
                            && (x.num_comanda >= numInicio && x.num_comanda <= numFinal)
                            && (x.razon_social.Contains(TxtCliente))).ToList();
            }
            else if ((FechaFinal >= FechaInicial) && (numFinal >= numInicio) && (numInicio > 0 && numFinal > 0))
            {
                lista = GuajiroEF.vw_lista_comandas.Where(x =>
                            (x.fecha >= FechaInicial && x.fecha <= FechaFinal)
                            && (x.num_comanda >= numInicio && x.num_comanda <= numFinal)).ToList();
            }
            else if ((FechaFinal >= FechaInicial) && (string.IsNullOrEmpty(TxtCliente) == false))
            {
                lista = GuajiroEF.vw_lista_comandas.Where(x =>
                            (x.fecha >= FechaInicial && x.fecha <= FechaFinal)
                            && (x.razon_social.Contains(TxtCliente))).ToList();
            }
            else if (FechaFinal >= FechaInicial)
            {
                lista = GuajiroEF.vw_lista_comandas.Where(x => (x.fecha >= FechaInicial && x.fecha <= FechaFinal)).ToList();
            }
            else
            {
                var vmMsj = new MensajeViewModel
                {
                    TituloMensaje = "Advertencia",
                    CuerpoMensaje = "Al menos debe elegir un período donde la Fecha Final sea mayor o igual a la Fecha Inicial",
                    MostrarCancelar = false,
                    TxtAceptar = "Aceptar"
                };
                var vwMsj = new MensajeView
                {
                    DataContext = vmMsj
                };
                var result = await DialogHost.Show(vwMsj, "Comandas");
            }
            ListaComandas = new ObservableCollection<vw_lista_comandas>(lista.OrderBy(x => x.num_comanda));
            CvsComandas = new CollectionViewSource
            {
                Source = ListaComandas
            };
        }

        private void filtarParaLlevar()
        {
            if (ListaComandas != null && ListaComandas.Count > 0)
            {
                if (EsParaLlevar == true)
                    CvsComandas = new CollectionViewSource
                    {
                        Source = ListaComandas.Where(x => x.para_llevar == EsParaLlevar).ToList()
                    };
                else
                    CvsComandas = new CollectionViewSource
                    {
                        Source = ListaComandas
                    };
            }
        }

        private async void MostrarDetalles(object parameter)
        {
            string idComanda = parameter as string;
            List<tbl_detallescomanda> lista = GuajiroEF.tbl_detallescomanda.Where(x => x.idcomanda.Equals(idComanda)).ToList();
            var vmDetCom = new DetallesComandaViewModel
            {
                ListaDetalles = new ObservableCollection<tbl_detallescomanda>(lista)
            };
            var vwDetCom = new DetallesComandaView
            {
                DataContext = vmDetCom
            };
            var result = await DialogHost.Show(vwDetCom, "Comandas");
        }
        #endregion
    }
}
