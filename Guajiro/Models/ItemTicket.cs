using System;
using System.Collections.Generic;

namespace Guajiro.Models
{
    public class ItemTicket
    {
        #region Variables
        private string _idLista;
        private string _idItem;
        private string _descripcion;
        private List<String> _guarniciones;
        private decimal? _precio;

        public string IdItem { get => _idItem; set => _idItem = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public List<string> Guarniciones { get => _guarniciones; set => _guarniciones = value; }
        public decimal? Precio { get => _precio; set => _precio = value; }
        public string IdLista { get => _idLista; set => _idLista = value; }
        #endregion

        #region Constructor
        public ItemTicket(string iditem, string descripcion, List<String> guarniciones, decimal? precio)
        {
            IdLista = Convert.ToString(Guid.NewGuid());
            IdItem = iditem;
            Descripcion = descripcion;
            Guarniciones = guarniciones;
            Precio = precio;
        }
        #endregion
    }
}
