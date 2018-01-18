using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Guajiro.ViewModels
{
    public class DatosClienteViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Commands
        public RelayCommand AgregarTelefonoCommand { get; set; }
        public RelayCommand AgregarDireccionCommand { get; set; }        
        #endregion

        #region Variables
        private string _txtNPrimario;
        private string _txtNSecundario;
        private string _txtPaterno;
        private string _txtMaterno;
        private string _txtNumTelefono;
        private string _txtCalle1;
        private string _txtCalle2;
        private string _txtInterior;
        private string _txtExterior;
        private string _txtColonia;
        private string _txtCPostal;
        private bool _chkEntrega;
        private bool _chkFactura;
        private tbl_listadoseldetalle _tipoTel;
        private ObservableCollection<tbl_listadoseldetalle> _tiposTelefono;
        private tbl_estados _estado;
        private ObservableCollection<tbl_estados> _listaEstados;
        private tbl_municipios _municipio;
        private ObservableCollection<tbl_municipios> _listaMunicipios;

        public string TxtNPrimario { get => _txtNPrimario; set { _txtNPrimario = value; OnPropertyChanged(); } }
        public string TxtNSecundario { get => _txtNSecundario; set { _txtNSecundario = value; OnPropertyChanged();} }
        public string TxtPaterno { get => _txtPaterno; set { _txtPaterno = value; OnPropertyChanged(); } }
        public string TxtMaterno { get => _txtMaterno; set { _txtMaterno = value; OnPropertyChanged(); } }
        public string TxtNumTelefono { get => _txtNumTelefono; set { _txtNumTelefono = value; OnPropertyChanged(); } }
        public string TxtCalle1 { get => _txtCalle1; set { _txtCalle1 = value; OnPropertyChanged(); } }
        public string TxtCalle2 { get => _txtCalle2; set { _txtCalle2 = value; OnPropertyChanged(); } }
        public string TxtInterior { get => _txtInterior; set { _txtInterior = value; OnPropertyChanged(); } }
        public string TxtExterior { get => _txtExterior; set { _txtExterior = value; OnPropertyChanged(); } }
        public string TxtColonia { get => _txtColonia; set { _txtColonia = value; OnPropertyChanged(); } }
        public string TxtCPostal { get => _txtCPostal; set { _txtCPostal = value; OnPropertyChanged(); } } 
        public bool ChkEntrega { get => _chkEntrega; set { _chkEntrega = value; OnPropertyChanged(); } }
        public bool ChkFactura { get => _chkFactura; set { _chkFactura = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_listadoseldetalle> TiposTelefono { get => _tiposTelefono; set { _tiposTelefono = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_estados> ListaEstados { get => _listaEstados; set { _listaEstados = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_municipios> ListaMunicipios { get => _listaMunicipios; set { _listaMunicipios = value; OnPropertyChanged(); } }
        public tbl_municipios Municipio { get => _municipio; set { _municipio = value; OnPropertyChanged(); } }
        public tbl_estados Estado { get => _estado; set { _estado = value; OnPropertyChanged(); } }
        public tbl_listadoseldetalle TipoTel { get => _tipoTel; set { _tipoTel = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public DatosClienteViewModel()
        {
            AgregarDireccionCommand = new RelayCommand(AgregarDireccion);
            AgregarTelefonoCommand = new RelayCommand(AgregarTelefono);
        }
        #endregion

        #region Métodos
        private void AgregarDireccion(object parameter)
        {

        }

        private void AgregarTelefono(object parameter)
        {

        }
        #endregion
    }
}
