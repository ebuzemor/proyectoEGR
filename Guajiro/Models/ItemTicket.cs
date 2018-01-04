using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Guajiro.Models
{
    public class ItemTicket : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Variables
        private string _idLista;
        private string _idItem;
        private string _descripcion;
        private List<String> _guarniciones;
        private decimal _precio;
        private int _cantidad;
        private decimal _importe;

        public string IdItem { get => _idItem; set { _idItem = value; NotifyPropertyChanged(); } }
        public string Descripcion { get => _descripcion; set { _descripcion = value; NotifyPropertyChanged(); } }
        public List<string> Guarniciones { get => _guarniciones; set { _guarniciones = value; NotifyPropertyChanged(); } }
        public decimal Precio { get => _precio; set { _precio = value; NotifyPropertyChanged(); } }
        public string IdLista { get => _idLista; set { _idLista = value; NotifyPropertyChanged(); } }
        public int Cantidad { get => _cantidad; set { _cantidad = value; NotifyPropertyChanged(); } }
        public decimal Importe { get => _importe; set { _importe = value; NotifyPropertyChanged(); } }
        #endregion

        #region Constructor
        public ItemTicket(string iditem, string descripcion, List<String> guarniciones, decimal precio)
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
