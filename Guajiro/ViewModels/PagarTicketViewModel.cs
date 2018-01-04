using System;
using Guajiro.Common;
using Guajiro.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Guajiro.ViewModels
{
    public class PagarTicketViewModel : Notifier
    {
        #region Commands
        #endregion

        #region Variables
        private decimal _totalTicket;
        private decimal _recibido;
        private decimal _cambio;
        private String _txtMensaje;
        private Boolean _verMensaje;
        private Boolean _activoBtnOk;

        public decimal TotalTicket { get => _totalTicket; set { _totalTicket = value; OnPropertyChanged("TotalTicket"); } }
        public decimal Recibido { get => _recibido; set { _recibido = value; OnPropertyChanged("Recibido"); ObtenerCambio(_recibido); } }
        public decimal Cambio { get => _cambio; set { _cambio = value; OnPropertyChanged("Cambio"); ActivarBtnOk(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged("TxtMensaje"); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged("VerMensaje"); } }
        public bool ActivoBtnOk { get => _activoBtnOk; set { _activoBtnOk = value; OnPropertyChanged("ActivoBtnOk"); } }
        #endregion

        #region Constructor
        public PagarTicketViewModel()
        {
            
        }

        #endregion

        #region Métodos
        private void ObtenerCambio(decimal cantidad)
        {
            if (Recibido > 0)
                Cambio = cantidad - TotalTicket;
            //if (Cambio < 0 || Cambio > Recibido)
            //    Cambio = 0;
            ActivarBtnOk();
        }

        private void ActualizarCambio(decimal? cantidad)
        {
            if (Recibido > 0)
                Cambio = Cambio + Convert.ToDecimal(cantidad);
            if (Cambio < 0 || Cambio > Recibido)
                Cambio = 0;
        }

        private void ActivarBtnOk()
        {
            ActivoBtnOk = (Cambio >= 0) ? true : false;
        }
        #endregion
    }
}
