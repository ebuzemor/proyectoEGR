using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace Guajiro.ViewModels
{
    public class PrincipalViewModel : Notifier
    {
        #region Commands
        public RelayCommand CerrarSesionCommand { get; set; }
        public RelayCommand SalirAppCommand { get; set; }
        #endregion

        #region Variables
        private tbl_usuarios _usuario;

        public MenuOpciones[] MenuOpcion { get; }
        public tbl_usuarios Usuario { get => _usuario; set { _usuario = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public PrincipalViewModel(tbl_usuarios UsuarioActual)
        {
            CerrarSesionCommand = new RelayCommand(CerrarSesion);
            SalirAppCommand = new RelayCommand(SalirApp);

            PuntoVentaViewModel vmPdV = new PuntoVentaViewModel
            {
                Usuario = UsuarioActual
            };
            PuntoVentaView vwPdV = new PuntoVentaView
            {
                DataContext = vmPdV
            };

            ComandasViewModel vmCom = new ComandasViewModel {};
            ComandasView vwCom = new ComandasView
            {
                DataContext = vmCom
            };

            ListaClientesViewModel vmLtaCte = new ListaClientesViewModel
            {
                CreaUsuario = UsuarioActual.idusuario
            };
            ListaClientesView vwLtaCte = new ListaClientesView
            {
                DataContext = vmLtaCte
            };

            ListaProveedoresViewModel vmLtaProv = new ListaProveedoresViewModel
            {
                CreaUsuario = UsuarioActual.idusuario
            };
            ListaProveedoresView vwLtaProv = new ListaProveedoresView
            {
                DataContext = vmLtaProv
            };

            MenuDiaViewModel vmMenuDia = new MenuDiaViewModel { };
            MenuDiaView vwMenuDia = new MenuDiaView {
                DataContext = vmMenuDia
            };

            InventarioViewModel vmInventario = new InventarioViewModel
            {
                IdPersona = UsuarioActual.idpersona
            };
            InventarioView vwInventario = new InventarioView
            {
                DataContext = vmInventario
            };

            MovimientosViewModel vmMovimiento = new MovimientosViewModel
            {
                IdPersona = UsuarioActual.idpersona
            };
            MovimientosView vwMovimiento = new MovimientosView
            {
                DataContext = vmMovimiento
            };
            
            MenuOpcion = new[]
            {
                new MenuOpciones("Cart", "Punto de Venta", vwPdV),
                new MenuOpciones("NoteText", "Comandas", vwCom),
                new MenuOpciones("ClipboardOutline", "Menú del Día", vwMenuDia),
                new MenuOpciones("AccountBox", "Clientes", vwLtaCte),
                new MenuOpciones("AccountCardDetails", "Proveedores", vwLtaProv),
                new MenuOpciones("FileCheck", "Inventario", vwInventario),
                //new MenuOpciones("TableLarge", "Movimientos", vwMovimiento)
                //new MenuOpciones("Facturas", new InventarioView()),
                //new MenuOpciones("Reportes", new InventarioView())
            };
        }
        #endregion

        #region Métodos
        private async void CerrarSesion(object parameter)
        {
            var vmMsj = new MensajeViewModel
            {
                TituloMensaje = "Advertencia",
                CuerpoMensaje = "¿Desea cerrar sesión?",
                MostrarCancelar = true,
                TxtAceptar = "Aceptar",
                TxtCancelar = "Cancelar"
            };
            var vwMsj = new MensajeView
            {
                DataContext = vmMsj
            };
            var cerrar = await DialogHost.Show(vwMsj, "Principal");
            if (cerrar.Equals("OK") == true)
            {
                LoginViewModel vmLogin = new LoginViewModel();
                LoginView login = new LoginView
                {
                    DataContext = vmLogin
                };
                Navigator.NavigationService.Navigate(login);
            }
        }

        private async void SalirApp(object parameter)
        {
            var vmMsj = new MensajeViewModel
            {
                TituloMensaje = "Advertencia",
                CuerpoMensaje = "¿Desea salir de la aplicación?",
                MostrarCancelar = true,
                TxtAceptar = "Aceptar",
                TxtCancelar = "Cancelar"
            };
            var vwMsj = new MensajeView
            {
                DataContext = vmMsj
            };
            var salir = await DialogHost.Show(vwMsj, "Principal");
            if(salir.Equals("OK")==true)
            {
                Application.Current.MainWindow.Close();
            }
        }
        #endregion
    }
}