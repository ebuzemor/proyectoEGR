using System;
using Guajiro.Common;
using Guajiro.Models;

namespace Guajiro.ViewModels
{
    public class EditarItemViewModel : Notifier
    {
        #region Variables
        private ItemTicket itemSeleccionado;
        private Boolean _activoBtnOk;
        private int _cantidad;
        private Decimal _importe;

        public ItemTicket ItemSeleccionado { get => itemSeleccionado; set { itemSeleccionado = value; OnPropertyChanged("ItemSeleccionado"); } }
        public bool ActivoBtnOk { get => _activoBtnOk; set { _activoBtnOk = value; OnPropertyChanged("ActivoBtnOk"); } }
        public int Cantidad
        {
            get => _cantidad;
            set
            {
                _cantidad = value;
                OnPropertyChanged("Cantidad");
                CalcularImporte(_cantidad);
                ActivarBtn();
            }
        }
        public decimal Importe { get => _importe; set { _importe = value; OnPropertyChanged("Importe"); } }
        #endregion

        #region Constructor
        public EditarItemViewModel() { }
        #endregion

        #region Métodos
        private void CalcularImporte(int cant)
        {
            try
            {
                if (cant > 0)
                    Importe = Convert.ToDecimal(cant * ItemSeleccionado.Precio);
                else
                    Importe = 0;
            }
            catch (Exception)
            {
                Importe = 0;
            }
        }

        private void ActivarBtn()
        {
            ActivoBtnOk = (Cantidad > 0) ? true : false;
        }

        public void ActualizarItem()
        {
            ItemSeleccionado.Cantidad = Cantidad;
            itemSeleccionado.Importe = Math.Round(Importe, 2);
        }
        #endregion
    }
}
