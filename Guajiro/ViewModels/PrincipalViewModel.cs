using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using System.Windows.Controls;


namespace Guajiro.ViewModels
{
    public class PrincipalViewModel : Notifier
    {
        public MenuOpciones[] MenuOpcion { get; }

        public PrincipalViewModel()
        {
            MenuOpcion = new[]
            {
                new MenuOpciones("Punto de Venta", new PuntoVentaView()),
                new MenuOpciones("Inventario", new InventarioView()),
                new MenuOpciones("Gastos", new PuntoVentaView()),
                new MenuOpciones("Facturación", new InventarioView())
            };
        }
    }
}
