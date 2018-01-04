using System;
using Guajiro.Common;
using Guajiro.Models;

namespace Guajiro.ViewModels
{
    public class MensajeViewModel : Notifier
    {
        #region Variables
        private String _tituloMensaje;
        private String _cuerpoMensaje;
        private String _txtAceptar;
        private String _txtCancelar;
        private Boolean _mostrarCancelar;

        public string TituloMensaje { get => _tituloMensaje; set { _tituloMensaje = value; OnPropertyChanged("TituloMensaje"); } }
        public string CuerpoMensaje { get => _cuerpoMensaje; set { _cuerpoMensaje = value; OnPropertyChanged("CuerpoMensaje"); } }
        public string TxtAceptar { get => _txtAceptar; set { _txtAceptar = value; OnPropertyChanged("TxtAceptar"); } }
        public string TxtCancelar { get => _txtCancelar; set { _txtCancelar = value; OnPropertyChanged("TxtCancelar"); } }
        public bool MostrarCancelar { get => _mostrarCancelar; set { _mostrarCancelar = value; OnPropertyChanged("MostrarCancelar"); } }
        #endregion

        #region Constructor
        public MensajeViewModel() { }
        #endregion
    }
}
