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
        public tbl_usuarios Usuario { get => _usuario; set { _usuario = value; OnPropertyChanged(); } }
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

            };
            MovimientosView vwMovimiento = new MovimientosView
            {
                DataContext = vmMovimiento
            };
            
            MenuOpcion = new[]
            {
                new MenuOpciones("Punto de Venta", vwPdV),
                new MenuOpciones("Comandas", vwCom),
                new MenuOpciones("Menú del Día", vwMenuDia),
                new MenuOpciones("Clientes", vwLtaCte),
                new MenuOpciones("Proveedores", vwLtaProv),
                new MenuOpciones("Inventario", vwInventario),                
                new MenuOpciones("Movimientos", vwMovimiento)
                //new MenuOpciones("Facturas", new InventarioView()),
                //new MenuOpciones("Reportes", new InventarioView())
            };
        }
        #endregion

    }
}
