using Guajiro.Common;
using Guajiro.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guajiro.ViewModels
{
    public class MenuDiaViewModel : Notifier
    {
        #region Commands
        public RelayCommand BuscarItemsCommand { get; set; }
        public RelayCommand DesayunosCommand { get; set; }
        public RelayCommand BebidasCommand { get; set; }
        public RelayCommand ComidasCommand { get; set; }
        public RelayCommand AgregarItemCommand { get; set; }
        public RelayCommand QuitarItemCommand { get; set; }        
        #endregion

        #region Variables
        private string _txtBuscar;
        private ObservableCollection<vw_lista_precios> _listaProductos;
        private string _idItemSel;
        private ObservableCollection<vw_lista_precios> _listaMenuDia;
        private string _txtMensaje;
        private bool _verMensaje;
        private DateTime _fechaMenu;

        public bd_guajiroEntities GuajiroEF;
        public string TxtBuscar { get => _txtBuscar; set { _txtBuscar = value; OnPropertyChanged("TxtBuscar"); } }
        public ObservableCollection<vw_lista_precios> ListaProductos { get => _listaProductos; set { _listaProductos = value; OnPropertyChanged("ListaProductos"); } }
        public string IdItemSel { get => _idItemSel; set { _idItemSel = value; OnPropertyChanged("IdItemSel"); } }
        public ObservableCollection<vw_lista_precios> ListaMenuDia { get => _listaMenuDia; set { _listaMenuDia = value; OnPropertyChanged("ListaMenuDia"); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged("TxtMensaje"); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged("VerMensaje"); } }
        public DateTime FechaMenu { get => _fechaMenu; set { _fechaMenu = value; OnPropertyChanged("FechaMenu"); } }
        #endregion

        #region Constructor
        public MenuDiaViewModel()
        {
            GuajiroEF = new bd_guajiroEntities();
            BuscarItemsCommand = new RelayCommand(BuscarItems);
            AgregarItemCommand = new RelayCommand(AgregarItem);
            QuitarItemCommand = new RelayCommand(QuitarItem);
            DesayunosCommand = new RelayCommand(CargarDesayunos);
            BebidasCommand = new RelayCommand(CargarBebidas);
            ComidasCommand = new RelayCommand(CargarComidas);
            ListaMenuDia = new ObservableCollection<vw_lista_precios>();
            FechaMenu = DateTime.Now;
        }
        #endregion

        #region Métodos
        private void BuscarItems(object parameter)
        {
            List<vw_lista_precios> datos = GuajiroEF.vw_lista_precios.Where(x => x.descripcion.Contains(TxtBuscar)).ToList();
            ListaProductos = new ObservableCollection<vw_lista_precios>(datos);
        }

        private void AgregarItem(object parameter)
        {
            IdItemSel = parameter as String;
            vw_lista_precios item = ListaMenuDia.SingleOrDefault(i => i.iditem == IdItemSel);
            if (item == null)
            {
                item = GuajiroEF.vw_lista_precios.Single(x => x.iditem == IdItemSel);
                ListaMenuDia.Add(item);
            }
            //else
            //{
            //    TxtMensaje = itemticket.Descripcion + " ya fue agregado, edite la cantidad";
            //    VerMensaje = true;
            //}
        }

        private void QuitarItem(object parameter)
        {
            String idlista = parameter as String;
            vw_lista_precios item = ListaMenuDia.SingleOrDefault(x => x.iditem == idlista);
            ListaMenuDia.Remove(item);
        }

        private void CargarBebidas(object parameter)
        {
            List<vw_lista_precios> prueba = GuajiroEF.vw_lista_precios.Where(x => x.tipo == "Bebidas").ToList();
            ListaProductos = new ObservableCollection<vw_lista_precios>(prueba);
        }

        private void CargarDesayunos(object parameter)
        {
            List<vw_lista_precios> prueba = GuajiroEF.vw_lista_precios.Where(x => x.tipo == "Desayuno").ToList();
            ListaProductos = new ObservableCollection<vw_lista_precios>(prueba);
        }

        private void CargarComidas(object parameter)
        {
            List<vw_lista_precios> prueba = GuajiroEF.vw_lista_precios.Where(x => x.tipo == "Comida").ToList();
            ListaProductos = new ObservableCollection<vw_lista_precios>(prueba);
        }
        #endregion
    }
}
