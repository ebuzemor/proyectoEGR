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
        public RelayCommand GuardarMenuCommand { get; set; }
        public RelayCommand CerrarMensajeCommand { get; set; }
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
            GuardarMenuCommand = new RelayCommand(GuardarMenu);
            CerrarMensajeCommand = new RelayCommand(CerrarMensaje);
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

        private async void GuardarMenu(object parameter)
        {
            try
            {
                if (ListaMenuDia.Count > 0)
                {
                    var lstmenu = GuajiroEF.tbl_menudeldia.ToList();
                    tbl_menudeldia checkmenu = lstmenu.SingleOrDefault(x => Convert.ToDateTime(x.fecha).Date == FechaMenu.Date);
                    if (checkmenu == null)
                    {
                        int ban = 0;
                        using (var bd = new bd_guajiroEntities())
                        {
                            tbl_menudeldia menudia = new tbl_menudeldia
                            {
                                idmenu = Convert.ToString(Guid.NewGuid()),
                                fecha = FechaMenu
                            };
                            bd.tbl_menudeldia.Add(menudia);

                            foreach (vw_lista_precios item in ListaMenuDia)
                            {
                                tbl_detallemenu detalle = new tbl_detallemenu
                                {
                                    iddetalle = Convert.ToString(Guid.NewGuid()),
                                    idmenu = menudia.idmenu,
                                    iditem = item.iditem
                                };
                                bd.tbl_detallemenu.Add(detalle);
                            }
                            ban = bd.SaveChanges();
                        }
                        if (ban > 0)
                        {
                            var vmMensaje = new MensajeViewModel
                            {
                                TituloMensaje = "Aviso",
                                CuerpoMensaje = "El menú del día ha sido guardado correctamente"
                            };
                            var vwMensaje = new MensajeView
                            {
                                DataContext = vmMensaje
                            };
                            var result = await DialogHost.Show(vwMensaje, "MenuDia");
                            ListaMenuDia.Clear();
                            FechaMenu = DateTime.Now;
                        }
                    }
                    else
                    {
                        TxtMensaje = "Ya se ha generado un menú de este día: " + FechaMenu.Date.ToShortDateString();
                        VerMensaje = true;
                    }
                }
                else
                {
                    TxtMensaje = "Debes agregar un platillo o bebida a la lista para guardarlo en el Menú del Día";
                    VerMensaje = true;
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void CerrarMensaje(object parameter) => VerMensaje = false;
        #endregion
    }
}
