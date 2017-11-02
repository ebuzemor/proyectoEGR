using System;
using System.Collections.Generic;
using System.Linq;
using Guajiro.Common;
using Guajiro.Models;
using System.Collections.ObjectModel;

namespace Guajiro.ViewModels
{
    public class PuntoVentaViewModel : Notifier
    {
        #region Variables

        private tbl_items _productos;
        private String _txtBuscar;
        private ObservableCollection<vw_lista_precios> _listaProductos;
        private ObservableCollection<ItemTicket> _listaTicket;
        private ObservableCollection<vw_lista_ordenes> _listaOrdenes;
        private vw_lista_precios _itemTicket;
        private String _idItemSel;
        private decimal _totalTicket;
        private decimal _recibido;
        private decimal _cambio;

        public tbl_items Productos { get => _productos; set => SetProperty(ref _productos, value); }
        public string TxtBuscar { get => _txtBuscar; set => SetProperty(ref _txtBuscar, value); }
        public ObservableCollection<vw_lista_precios> ListaProductos { get => _listaProductos; set { _listaProductos = value; OnPropertyChanged("ListaProductos"); } }
        public ObservableCollection<vw_lista_ordenes> ListaOrdenes { get => _listaOrdenes; set { _listaOrdenes = value; OnPropertyChanged("ListaOrdenes"); } }
        public ObservableCollection<ItemTicket> ListaTicket { get => _listaTicket; set { _listaTicket = value; OnPropertyChanged("ListaTicket"); } }
        public string IdItemSel { get => _idItemSel; set => SetProperty(ref _idItemSel, value); }
        public vw_lista_precios ItemTicket { get => _itemTicket; set => SetProperty(ref _itemTicket, value); }
        public decimal TotalTicket { get => _totalTicket; set { _totalTicket = value; OnPropertyChanged("TotalTicket"); } }
        public decimal Recibido { get => _recibido; set { _recibido = value; OnPropertyChanged("Recibido"); ObtenerCambio(_recibido); } }
        public decimal Cambio { get => _cambio; set { _cambio = value; OnPropertyChanged("Cambio"); } }

        public RelayCommand BuscarItemsCommand { get; set; }
        public RelayCommand AgregarItemCommand { get; set; }
        public RelayCommand QuitarItemCommand { get; set; }
        public RelayCommand EditarItemCommand { get; set; }

        public bd_guajiroEntities guajiroEF;

        #endregion

        #region Constructor

        public PuntoVentaViewModel()
        {
            guajiroEF = new bd_guajiroEntities();
            BuscarItemsCommand = new RelayCommand(BuscarItems);
            AgregarItemCommand = new RelayCommand(AgregarItem);
            QuitarItemCommand = new RelayCommand(QuitarItem);
            EditarItemCommand = new RelayCommand(EditarItem);
            ListaTicket = new ObservableCollection<ItemTicket>();
        }

        #endregion

        #region Metodos

        private void BuscarItems(object parameter)
        {
            List<vw_lista_precios> prueba = guajiroEF.vw_lista_precios.Where(x => x.descripcion.Contains(TxtBuscar)).ToList();
            ListaProductos = new ObservableCollection<vw_lista_precios>(prueba);
        }

        private void AgregarItem(object parameter)
        {
            IdItemSel = parameter as String;
            List<vw_lista_ordenes> ordenes = guajiroEF.vw_lista_ordenes.Where(x => x.iditem == IdItemSel).ToList();
            ListaOrdenes = new ObservableCollection<vw_lista_ordenes>(ordenes);
            vw_lista_precios item = guajiroEF.vw_lista_precios.Find(IdItemSel);
            List<String> guarniciones = new List<string>();
            foreach (var g in ordenes)
                guarniciones.Add(g.nombreguarnicion);
            ItemTicket itemticket = new ItemTicket(item.iditem, item.descripcion, guarniciones, item.valor);
            ListaTicket.Add(itemticket);
            ObtenerMontos(item.valor);
            ObtenerCambio(Recibido);
        }

        private void QuitarItem(object parameter)
        {
            String idlista = parameter as String;
            ItemTicket elem = ListaTicket.Single(x => x.IdLista == idlista);
            ActualizarMontos(elem.Precio);
            ActualizarCambio(elem.Precio);
            ListaTicket.Remove(elem);
        }

        private void EditarItem(object parameter)
        {

        }

        private void ObtenerMontos(decimal? cantidad)
        {
            TotalTicket = TotalTicket + Convert.ToDecimal(cantidad);
        }

        private void ActualizarMontos(decimal? cantidad)
        {
            TotalTicket = TotalTicket - Convert.ToDecimal(cantidad);
        }

        private void ObtenerCambio(decimal cantidad)
        {
            if (Recibido > 0)
                Cambio = cantidad - TotalTicket;
            if (Cambio < 0 || Cambio > Recibido)
                Cambio = 0;
        }

        private void ActualizarCambio(decimal? cantidad)
        {
            if (Recibido > 0)
                Cambio = Cambio + Convert.ToDecimal(cantidad);
            if (Cambio < 0 || Cambio > Recibido)
                Cambio = 0;
        }

        #endregion
    }
}
