using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Guajiro.ViewModels
{
    public class PuntoVentaViewModel : Notifier
    {
        #region Commands
        public RelayCommand BuscarItemsCommand { get; set; }
        public RelayCommand AgregarItemCommand { get; set; }
        public RelayCommand QuitarItemCommand { get; set; }
        public RelayCommand EditarItemCommand { get; set; }
        public RelayCommand CerrarMensajeCommand { get; set; }
        public RelayCommand CancelarTicketCommand { get; set; }
        public RelayCommand GuardarTicketCommand { get; set; }
        public RelayCommand ClienteExistenteCommand { get; set; }
        public RelayCommand ComensalCommand { get; set; }
        public RelayCommand BebidasCommand { get; set; }
        public RelayCommand DesayunosCommand { get; set; }
        public RelayCommand ComidasCommand { get; set; }
        public RelayCommand MenuDiaCommand { get; set; }
        #endregion

        #region Variables
        private tbl_items _productos;
        private String _txtBuscar;
        private String _comanda;
        private ObservableCollection<vw_lista_precios> _listaProductos;
        private ObservableCollection<ItemTicket> _listaTicket;
        private ObservableCollection<vw_lista_ordenes> _listaOrdenes;
        private ObservableCollection<vw_lista_clientes> _listaClientes;
        private vw_lista_clientes _cliente;
        private ObservableCollection<tbl_mesas> _listaMesas;
        private String _clienteDireccion;
        private vw_lista_direcciones _infoDireccion;
        private tbl_mesas _mesa;
        private vw_lista_precios _itemTicket;
        private String _idItemSel;
        private String _txtMensaje;
        private Boolean _verMensaje;
        private decimal _totalTicket;
        private decimal _recibido;
        private decimal _cambio;
        private String _nombreCliente;
        private DateTime _fechaPago;
        private Boolean _esParaComerAqui;
        private int _numComanda;
        private tbl_usuarios _usuario;

        public tbl_items Productos { get => _productos; set { _productos = value; OnPropertyChanged(); } }
        public string TxtBuscar { get => _txtBuscar; set { _txtBuscar = value; OnPropertyChanged(); } }
        public ObservableCollection<vw_lista_precios> ListaProductos { get => _listaProductos; set { _listaProductos = value; OnPropertyChanged(); } }
        public ObservableCollection<vw_lista_ordenes> ListaOrdenes { get => _listaOrdenes; set { _listaOrdenes = value; OnPropertyChanged(); } }
        public ObservableCollection<ItemTicket> ListaTicket { get => _listaTicket; set { _listaTicket = value; OnPropertyChanged(); } }
        public string IdItemSel { get => _idItemSel; set { _idItemSel = value; OnPropertyChanged(); } }
        public vw_lista_precios ItemTicket { get => _itemTicket; set { _itemTicket = value; OnPropertyChanged(); } }
        public decimal TotalTicket { get => _totalTicket; set { _totalTicket = value; OnPropertyChanged(); } }
        public decimal Recibido { get => _recibido; set { _recibido = value; OnPropertyChanged(); ObtenerCambio(_recibido); } }
        public decimal Cambio { get => _cambio; set { _cambio = value; OnPropertyChanged(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged(); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged(); } }
        public string Comanda { get => _comanda; set { _comanda = value; OnPropertyChanged(); } }
        public ObservableCollection<vw_lista_clientes> ListaClientes { get => _listaClientes; set { _listaClientes = value; OnPropertyChanged(); } }
        public vw_lista_clientes Cliente { get => _cliente; set { _cliente = value; OnPropertyChanged(); ActualizarDireccion(); } }
        public ObservableCollection<tbl_mesas> ListaMesas { get => _listaMesas; set { _listaMesas = value; OnPropertyChanged(); } }
        public tbl_mesas Mesa { get => _mesa; set { _mesa = value; OnPropertyChanged(); } }
        public string ClienteDireccion { get => _clienteDireccion; set { _clienteDireccion = value; OnPropertyChanged(); } }
        public vw_lista_direcciones InfoDireccion { get => _infoDireccion; set { _infoDireccion = value; OnPropertyChanged(); } }
        public string NombreCliente { get => _nombreCliente; set { _nombreCliente = value; OnPropertyChanged(); } }
        public DateTime FechaPago { get => _fechaPago; set { _fechaPago = value; OnPropertyChanged(); } }
        public bool EsParaComerAqui { get => _esParaComerAqui; set { _esParaComerAqui = value; OnPropertyChanged(); } }
        public int NumComanda { get => _numComanda; set { _numComanda = value; OnPropertyChanged(); } }
        public tbl_usuarios Usuario { get => _usuario; set { _usuario = value; OnPropertyChanged(); } }

        public bd_guajiroEntities GuajiroEF;
        #endregion

        #region Constructor

        public PuntoVentaViewModel()
        {
            GuajiroEF = new bd_guajiroEntities();
            BuscarItemsCommand = new RelayCommand(BuscarItems);
            AgregarItemCommand = new RelayCommand(AgregarItem);
            QuitarItemCommand = new RelayCommand(QuitarItem);
            EditarItemCommand = new RelayCommand(EditarItem);
            CerrarMensajeCommand = new RelayCommand(CerrarMensaje);
            CancelarTicketCommand = new RelayCommand(CancelarTicket);
            GuardarTicketCommand = new RelayCommand(GuardarTicket);
            ClienteExistenteCommand = new RelayCommand(ClienteExistente);
            ComensalCommand = new RelayCommand(Comensal);
            BebidasCommand = new RelayCommand(CargarBebidas);
            DesayunosCommand = new RelayCommand(CargarDesayunos);
            ComidasCommand = new RelayCommand(CargarComidas);
            MenuDiaCommand = new RelayCommand(CargarMenuDelDia);
            ListaTicket = new ObservableCollection<ItemTicket>();
            List<tbl_mesas> mesas = GuajiroEF.tbl_mesas.ToList();
            ListaMesas = new ObservableCollection<tbl_mesas>(mesas);
            ListaMesas.Insert(0, new tbl_mesas
            {
                idmesa = "0",
                desc_mesa = "Para llevar",
                num_mesa = 0
            });
            Mesa = ListaMesas.First();
            List<vw_lista_clientes> clientes = GuajiroEF.vw_lista_clientes.ToList();
            ListaClientes = new ObservableCollection<vw_lista_clientes>(clientes);
            FechaPago = DateTime.Now;
            EsParaComerAqui = true;
            int numcom = GuajiroEF.tbl_comandas.ToList().Count;
            if (numcom > 0)
                NumComanda = GuajiroEF.tbl_comandas.Max(x => x.num_comanda) + 1;
            else
                NumComanda = 0;
        }

        #endregion

        #region Metodos

        private void BuscarItems(object parameter)
        {
            List<vw_lista_precios> prueba = GuajiroEF.vw_lista_precios.Where(x => x.descripcion.Contains(TxtBuscar)).ToList();
            ListaProductos = new ObservableCollection<vw_lista_precios>(prueba);
        }

        private void AgregarItem(object parameter)
        {
            IdItemSel = parameter as String;
            ItemTicket itemticket = ListaTicket.SingleOrDefault(e => e.IdItem == IdItemSel);
            if (itemticket == null)
            {
                List<vw_lista_ordenes> ordenes = GuajiroEF.vw_lista_ordenes.Where(x => x.iditem == IdItemSel).ToList();
                ListaOrdenes = new ObservableCollection<vw_lista_ordenes>(ordenes);
                vw_lista_precios item = GuajiroEF.vw_lista_precios.Find(IdItemSel);
                List<String> guarniciones = new List<string>();
                foreach (var g in ordenes)
                    guarniciones.Add(g.nombreguarnicion);
                itemticket = new ItemTicket(item.iditem, item.descripcion, guarniciones, Convert.ToDecimal(item.valor))
                {
                    Cantidad = 1
                };
                itemticket.Importe = Convert.ToDecimal(itemticket.Cantidad * item.valor);
                ListaTicket.Add(itemticket);
                ActualizarMontos();
                ObtenerCambio(Recibido);
            }
            else
            {
                TxtMensaje = itemticket.Descripcion + " ya fue agregado, edite la cantidad";
                VerMensaje = true;
            }
        }

        private void QuitarItem(object parameter)
        {
            String idlista = parameter as String;
            ItemTicket elem = ListaTicket.Single(x => x.IdLista == idlista);
            ActualizarCambio(elem.Precio);
            ListaTicket.Remove(elem);
            ActualizarMontos();
        }

        private async void EditarItem(object parameter)
        {
            String idLista = parameter as String;
            ItemTicket editItem = ListaTicket.Single(e => e.IdLista == idLista);
            var vmEditar = new EditarItemViewModel
            {
                ItemSeleccionado = editItem,
                Cantidad = editItem.Cantidad,
                Importe=editItem.Importe
            };
            var vwEditar = new EditarItemView
            {
                DataContext = vmEditar
            };
            var result = await DialogHost.Show(vwEditar, "PuntoVenta");
            if (result.Equals("OK") == true)
            {
                vmEditar.ActualizarItem();
                editItem = vmEditar.ItemSeleccionado;
                ActualizarMontos();
                ObtenerCambio(Recibido);
            }
        }

        private void CerrarMensaje(object parameter) => VerMensaje = false;

        private void ActualizarMontos()
        {
            TotalTicket = 0;
            foreach(ItemTicket i in ListaTicket)
            {
                TotalTicket += i.Importe;
            }
            //TotalTicket = TotalTicket + Convert.ToDecimal(cantidad);
        }

        private async void CancelarTicket(object parameter)
        {
            if (ListaTicket.Count > 0)
            {
                var vmMensaje = new MensajeViewModel
                {
                    TituloMensaje = "Advertencia",
                    CuerpoMensaje = "¿Desea cancelar la orden?",
                    MostrarCancelar=true,
                    TxtAceptar="SI",
                    TxtCancelar="NO"
                };
                var vwMensaje = new MensajeView
                {
                    DataContext = vmMensaje
                };
                var result = await DialogHost.Show(vwMensaje, "PuntoVenta");
                if (result.Equals("OK") == true)
                {
                    LimpiarCampos();
                }
            }
        }

        private void LimpiarCampos()
        {
            ListaTicket.Clear();
            TotalTicket = 0;
            Recibido = 0;
            Cambio = 0;
            FechaPago = DateTime.Now;
            Mesa = ListaMesas.First();
            NumComanda = GuajiroEF.tbl_comandas.Max(x => x.num_comanda) + 1;
            EsParaComerAqui = true;
            ClienteDireccion = string.Empty;
        }

        private void ObtenerCambio(decimal cantidad)
        {
            if (Recibido > 0)
                Cambio = cantidad - TotalTicket;
            if (Cambio < 0 || Cambio > Recibido)
                Cambio = 0;
        }

        private void ActualizarCambio(decimal cantidad)
        {
            if (Recibido > 0)
                Cambio = Cambio + Convert.ToDecimal(cantidad);
            if (Cambio < 0 || Cambio > Recibido)
                Cambio = 0;
        }

        private async void GuardarTicket(Object parameter)
        {
            try
            {
                if (ListaTicket.Count > 0)
                {
                    //idcomanda, fecha, num_comanda, idmesa, total, para_llevar, nombre, crea_usuario
                    //iddetalle, idcomanda, iditem, idobservaciones, descripcion, precio
                    int guardados = 0;
                    using (var bd = new bd_guajiroEntities())
                    {
                        tbl_comandas comanda = new tbl_comandas
                        {
                            idcomanda = Convert.ToString(Guid.NewGuid()),
                            fecha = FechaPago,
                            num_comanda = 0,
                            idmesa = Convert.ToString(Mesa.idmesa),
                            para_llevar = !EsParaComerAqui,
                            idpersona = (EsParaComerAqui == true) ? "1c87a56f-e479-11e7-8cd6-204747335338" : Cliente.idpersona,
                            total = TotalTicket,
                            crea_usuario = Usuario.idusuario
                        };
                        bd.tbl_comandas.Add(comanda);
                        foreach (ItemTicket item in ListaTicket)
                        {
                            tbl_detallescomanda detalle = new tbl_detallescomanda
                            {
                                iddetalle = Convert.ToString(Guid.NewGuid()),
                                iditem = item.IdItem,
                                idcomanda = comanda.idcomanda,
                                descripcion = item.Descripcion,
                                observaciones = string.Empty,
                                precio = item.Precio
                            };
                            bd.tbl_detallescomanda.Add(detalle);
                        }
                        tbl_movimientos movingreso = new tbl_movimientos
                        {
                            idmovimiento = Convert.ToString(Guid.NewGuid()),
                            idlstipomovimiento = "e47c55ba-368a-11e7-b904-204747335338", //ingreso
                            fecha = DateTime.Now,
                            descripcion = "Venta de Comida",
                            monto = TotalTicket,
                            crea_usuario = Usuario.idusuario
                        };
                        bd.tbl_movimientos.Add(movingreso);
                        var vmPagar = new PagarTicketViewModel()
                        {
                            TotalTicket = TotalTicket
                        };
                        var vwPagar = new PagarTicketView
                        {
                            DataContext = vmPagar
                        };
                        var resultado = await DialogHost.Show(vwPagar, "PuntoVenta");
                        if (resultado.Equals("OK") == true)
                        {
                            guardados = bd.SaveChanges();
                        }
                    }
                    if(guardados > 0)
                    {
                        var vmMensaje = new MensajeViewModel
                        {
                            TituloMensaje = "Aviso",
                            CuerpoMensaje = "La orden se ha guardado correctamente",
                            TxtAceptar = "Aceptar"
                        };
                        var vwMensaje = new MensajeView
                        {
                            DataContext = vmMensaje
                        };
                        var result = await DialogHost.Show(vwMensaje, "PuntoVenta");
                        LimpiarCampos();
                    }
                }
                else
                {
                    TxtMensaje = "Debes agregar al menos un item a la lista";
                    VerMensaje = true;
                }
            }
            catch(Exception ex)
            {
                TxtMensaje = "Error: " + ex.Message;
                VerMensaje = true;
            }
        }

        private void CerrarVentana(object sender, DialogClosingEventArgs eventArgs)
        {
            var respuesta = eventArgs.Parameter;
            if(respuesta.Equals("OK") == true)
            {
                var x = eventArgs.Session.Content;
                var s = sender;
            }
            Console.Write(respuesta);
        }

        private void ActualizarDireccion()
        {
            if (Cliente != null)
            {
                InfoDireccion = GuajiroEF.vw_lista_direcciones.SingleOrDefault(x => x.idpersona == Cliente.idpersona);
                ClienteDireccion = (InfoDireccion != null) ? InfoDireccion.direccion : "No tiene dirección registrada.";
            }
            else
            {
                ClienteDireccion = string.Empty;
            }
        }

        private void ClienteExistente(object parameter)
        {
            ActualizarDireccion();
            Mesa = ListaMesas.First();
        }

        private void Comensal(object parameter)
        {
            Mesa = ListaMesas.ElementAt(1);
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

        private async void CargarMenuDelDia(object parameter)
        {
            var datos = GuajiroEF.tbl_menudeldia.ToList();
            tbl_menudeldia menu = datos.SingleOrDefault(x => x.fecha.Value.Date == DateTime.Today.Date);
            if (menu != null)
            {
                List<tbl_detallemenu> detmenu = GuajiroEF.tbl_detallemenu.Where(x => x.idmenu == menu.idmenu).ToList();
                ListaProductos = new ObservableCollection<vw_lista_precios>();
                foreach (tbl_detallemenu d in detmenu)
                {
                    vw_lista_precios vwlista = GuajiroEF.vw_lista_precios.Single(x => x.iditem == d.iditem);
                    ListaProductos.Add(vwlista);
                }
            }
            else
            {
                var vmMensaje = new MensajeViewModel
                {
                    TituloMensaje = "Error",
                    CuerpoMensaje = "Aún no hay menú del día",
                    MostrarCancelar = false,
                    TxtAceptar = "Aceptar"
                };
                var vwMensaje = new MensajeView
                {
                    DataContext = vmMensaje
                };
                var result = await DialogHost.Show(vwMensaje, "PuntoVenta");
            }
        }
        
        #endregion
    }
}
