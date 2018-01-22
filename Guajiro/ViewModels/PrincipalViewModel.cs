using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;

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
        public tbl_usuarios Usuario { get => _usuario; set { _usuario = value; OnPropertyChanged("Usuario"); } }
        #endregion


        #region Constructor
        public PrincipalViewModel(tbl_usuarios UsuarioActual)
        {
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
                IdPersona = UsuarioActual.idpersona
            };
            ListaClientesView vwLtaCte = new ListaClientesView
            {
                DataContext = vmLtaCte
            };

            ListaProveedoresViewModel vmLtaProv = new ListaProveedoresViewModel
            {
                IdPersona = UsuarioActual.idpersona
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
            
            MenuOpcion = new[]
            {
                new MenuOpciones("Punto de Venta", vwPdV),
                new MenuOpciones("Comandas", vwCom),
                new MenuOpciones("Menú del Día", vwMenuDia),
                new MenuOpciones("Clientes", vwLtaCte),
                new MenuOpciones("Proveedores", vwLtaProv),
                new MenuOpciones("Inventario", vwInventario),
                //new MenuOpciones("Facturas", new InventarioView()),
                new MenuOpciones("Registrar Gastos", new RegistrarGastosView()),
                new MenuOpciones("Registrar Compras", new InventarioView()),
                new MenuOpciones("Movimientos", new InventarioView())
                //new MenuOpciones("Reportes", new InventarioView())
            };
        }
        #endregion

    }
}
