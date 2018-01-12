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

            ListaClientesViewModel vmLtaCte = new ListaClientesViewModel { };
            ListaClientesView vwLtaCte = new ListaClientesView
            {
                DataContext = vmLtaCte
            };

            MenuOpcion = new[]
            {
                new MenuOpciones("Punto de Venta", vwPdV),
                new MenuOpciones("Comandas", vwCom),
                new MenuOpciones("Menú del Día", new MenuDiaView()),
                new MenuOpciones("Clientes", vwLtaCte),
                new MenuOpciones("Proveedores", new ListaProveedoresView()),
                new MenuOpciones("Inventario", new InventarioView()),
                new MenuOpciones("Movimientos", new InventarioView()),
                new MenuOpciones("Facturas", new InventarioView()),
                new MenuOpciones("Lista de Compras", new InventarioView()),
                new MenuOpciones("Reportes", new InventarioView())
            };
        }
        #endregion

    }
}
