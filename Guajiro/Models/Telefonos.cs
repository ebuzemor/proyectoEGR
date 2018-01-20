using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guajiro.Common;

namespace Guajiro.Models
{
    public class Telefonos : Notifier
    {
        #region Variables
        private string _idlsTipoTelefono;
        private string _idPersona;
        private string _idTelefono;
        private string _numTelefono;
        private string _tipoTel;
        
        public string IdlsTipoTelefono { get => _idlsTipoTelefono; set { _idlsTipoTelefono = value; OnPropertyChanged("IdlsTipoTelefono"); } }
        public string IdPersona { get => _idPersona; set { _idPersona = value; OnPropertyChanged("IdPersona"); } }
        public string IdTelefono { get => _idTelefono; set { _idTelefono = value; OnPropertyChanged("IdTelefono"); } }
        public string NumTelefono { get => _numTelefono; set { _numTelefono = value; OnPropertyChanged("NumTelefono"); } }
        public string TipoTel { get => _tipoTel; set { _tipoTel = value; OnPropertyChanged("TipoTel"); } }
        #endregion

    }
}
