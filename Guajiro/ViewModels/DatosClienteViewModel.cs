using Guajiro.Common;
using Guajiro.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Guajiro.ViewModels
{
    public class DatosClienteViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public RelayCommand AgregarTelefonoCommand { get; set; }
        public RelayCommand BorrarTelefonoCommand { get; set; }
        public RelayCommand AgregarDireccionCommand { get; set; }
        public RelayCommand EditarDireccionCommand { get; set; }
        public RelayCommand BorrarDireccionCommand { get; set; }
        public RelayCommand CerrarMensajeCommand { get; set; }
        public RelayCommand GuardarClienteCommand { get; set; }
        #endregion

        #region Variables
        private string _txtNPrimario;
        private string _txtNSecundario;
        private string _txtPaterno;
        private string _txtMaterno;
        private string _txtRFC;
        private string _txtRazonSocial;
        private string _txtEmail;
        private string _txtNumTelefono;
        private string _txtCalle1;
        private string _txtCalle2;
        private string _txtInterior;
        private string _txtExterior;
        private string _txtColonia;
        private string _txtCPostal;
        private string _txtMensaje;
        private string _idPersona;
        private string _creaUsuario;
        private string _tipoPersona;
        private bool _verMensaje;
        private bool _chkFisica;
        private bool _chkMoral;
        private bool _chkEntrega;
        private bool _chkFactura;
        private ObservableCollection<tbl_listadoseldetalle> _tiposTelefono;
        private ObservableCollection<tbl_estados> _listaEstados;
        private ObservableCollection<tbl_municipios> _listaMunicipios;
        private ObservableCollection<vw_lista_telefonos> _listaTelefonos;
        private ObservableCollection<tbl_direcciones> _listaDirecciones;
        private tbl_listadoseldetalle _tipoTel;
        private tbl_estados _estado;
        private tbl_municipios _municipio;
        private vw_lista_telefonos _datosTel;
        private tbl_direcciones _datosDir;

        public bd_guajiroEntities GuajiroEF;
        public string TxtNPrimario { get => _txtNPrimario; set { _txtNPrimario = value; OnPropertyChanged(); } }
        public string TxtNSecundario { get => _txtNSecundario; set { _txtNSecundario = value; OnPropertyChanged();} }
        public string TxtPaterno { get => _txtPaterno; set { _txtPaterno = value; OnPropertyChanged(); } }
        public string TxtMaterno { get => _txtMaterno; set { _txtMaterno = value; OnPropertyChanged(); } }
        public string TxtRFC { get => _txtRFC; set { _txtRFC = value; OnPropertyChanged(); } }
        public string TxtRazonSocial { get => _txtRazonSocial; set { _txtRazonSocial = value; OnPropertyChanged(); } }
        public string TxtEmail { get => _txtEmail; set { _txtEmail = value; OnPropertyChanged(); } }
        public string TxtNumTelefono { get => _txtNumTelefono; set { _txtNumTelefono = value; OnPropertyChanged(); } }
        public string TxtCalle1 { get => _txtCalle1; set { _txtCalle1 = value; OnPropertyChanged(); } }
        public string TxtCalle2 { get => _txtCalle2; set { _txtCalle2 = value; OnPropertyChanged(); } }
        public string TxtInterior { get => _txtInterior; set { _txtInterior = value; OnPropertyChanged(); } }
        public string TxtExterior { get => _txtExterior; set { _txtExterior = value; OnPropertyChanged(); } }
        public string TxtColonia { get => _txtColonia; set { _txtColonia = value; OnPropertyChanged(); } }
        public string TxtCPostal { get => _txtCPostal; set { _txtCPostal = value; OnPropertyChanged(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged(); } }
        public string IdPersona { get => _idPersona; set { _idPersona = value; OnPropertyChanged(); } }
        public string TipoPersona { get => _tipoPersona; set { _tipoPersona = value; OnPropertyChanged(); } }
        public bool ChkFisica
        {
            get => _chkFisica;
            set
            {
                _chkFisica = value;
                OnPropertyChanged();
                TipoPersona = (_chkFisica == true) ? "e0eaa02f-fe83-11e7-83f1-204747335338" : "e0e8f331-fe83-11e7-83f1-204747335338";
                TxtRazonSocial = "";
            }
        }
        public bool ChkMoral
        {
            get => _chkMoral;
            set
            {
                _chkMoral = value;
                OnPropertyChanged();
                TipoPersona = (_chkMoral == true) ? "e0e8f331-fe83-11e7-83f1-204747335338" : "e0eaa02f-fe83-11e7-83f1-204747335338";
                TxtNPrimario = ""; TxtNSecundario = ""; TxtPaterno = ""; TxtMaterno = "";
            }
        }
        public bool ChkEntrega { get => _chkEntrega; set { _chkEntrega = value; OnPropertyChanged(); } }
        public bool ChkFactura { get => _chkFactura; set { _chkFactura = value; OnPropertyChanged(); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_listadoseldetalle> TiposTelefono { get => _tiposTelefono; set { _tiposTelefono = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_estados> ListaEstados { get => _listaEstados; set { _listaEstados = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_municipios> ListaMunicipios { get => _listaMunicipios; set { _listaMunicipios = value; OnPropertyChanged(); } }
        public ObservableCollection<vw_lista_telefonos> ListaTelefonos { get => _listaTelefonos; set { _listaTelefonos = value; OnPropertyChanged(); } }
        public ObservableCollection<tbl_direcciones> ListaDirecciones { get => _listaDirecciones; set { _listaDirecciones = value; OnPropertyChanged(); } }
        public tbl_municipios Municipio { get => _municipio; set { _municipio = value; OnPropertyChanged(); } }
        public tbl_estados Estado { get => _estado; set { _estado = value; OnPropertyChanged();  FiltrarMunicipios(value.idestado); } }
        public tbl_listadoseldetalle TipoTel { get => _tipoTel; set { _tipoTel = value; OnPropertyChanged(); } }
        public vw_lista_telefonos DatosTel { get => _datosTel; set { _datosTel = value; OnPropertyChanged(); } }
        public tbl_direcciones DatosDir { get => _datosDir; set { _datosDir = value; OnPropertyChanged(); } }
        public string CreaUsuario { get => _creaUsuario; set { _creaUsuario = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public DatosClienteViewModel()
        {            
            AgregarTelefonoCommand = new RelayCommand(AgregarTelefono);
            BorrarTelefonoCommand = new RelayCommand(BorrarTelefono);
            AgregarDireccionCommand = new RelayCommand(AgregarDireccion);
            EditarDireccionCommand = new RelayCommand(EditarDireccion);
            BorrarDireccionCommand = new RelayCommand(BorrarDireccion);
            CerrarMensajeCommand = new RelayCommand(CerrarMensaje);
            GuardarClienteCommand = new RelayCommand(GuardarCliente);
            GuajiroEF = new bd_guajiroEntities();
            ListaTelefonos = new ObservableCollection<vw_lista_telefonos>();
            ListaDirecciones = new ObservableCollection<tbl_direcciones>();
            ChkEntrega = false;
            ChkFactura = false;
            ChkFisica = true;
            ChkMoral = false;
        }        
        #endregion

        #region Métodos
        private void AgregarDireccion(object parameter)
        {
            tbl_direcciones dir = ListaDirecciones.SingleOrDefault(x => x.calle1 == TxtCalle1 && x.colonia == TxtColonia
                                    && x.codigopostal == TxtCPostal && x.idmunicipio == Municipio.idmunicipio);
            if(dir != null)
            {
                TxtMensaje = "Existe una dirección con datos similares, verifique por favor";
                VerMensaje = true;
            }
            else
            {
                bool error = ErrorDatosDireccion();
                if (error != true)
                {
                    DatosDir = new tbl_direcciones
                    {
                        iddireccion = Convert.ToString(Guid.NewGuid()),
                        calle1 = TxtCalle1,
                        calle2 = TxtCalle2,
                        interior = TxtInterior,
                        exterior = TxtExterior,
                        colonia = TxtColonia,
                        codigopostal = TxtCPostal,
                        idmunicipio = Municipio.idmunicipio,
                        entrega = ChkEntrega,
                        fiscal = ChkFactura,
                        idpersona = IdPersona
                    };
                    ListaDirecciones.Add(DatosDir);
                    using(var bd = new bd_guajiroEntities())
                    {
                        bd.tbl_direcciones.Add(DatosDir);
                        int c = bd.SaveChanges();
                        if (c > 0)
                        {
                            TxtMensaje = "La dirección fue agregada correctamente";
                            VerMensaje = true;
                        }
                    }
                    LimpiarDireccion();
                }
                else
                {
                    TxtMensaje = "Los siguientes campos son obligatorios Calle1, Núm. Exterior, Colonia, Código Postal, Municipio.";
                    VerMensaje = true;
                }
            }
        }

        private void EditarDireccion(object parameter)
        {
            string iddir = parameter as string;
            tbl_direcciones dir = GuajiroEF.tbl_direcciones.SingleOrDefault(x => x.iddireccion == iddir);
            if (dir != null)
            {
                TxtCalle1 = dir.calle1;
                TxtCalle2 = dir.calle2;
                TxtInterior = dir.interior;
                TxtExterior = dir.exterior;
                TxtColonia = dir.colonia;
                TxtCPostal = dir.codigopostal;
                Municipio = GuajiroEF.tbl_municipios.Single(m => m.idmunicipio == dir.idmunicipio);
                Estado = ListaEstados.Single(e => e.idestado == Municipio.idestado);
                ChkEntrega = (bool)dir.entrega;
                ChkFactura = (bool)dir.fiscal;
            }
        }

        private void BorrarDireccion(object parameter)
        {
            
        }

        private void LimpiarDireccion()
        {
            TxtCalle1 = "";
            TxtCalle2 = "";
            TxtColonia = "";
            TxtCPostal = "";
            TxtExterior = "";
            TxtInterior = "";
            ListaMunicipios.Clear();
            ChkEntrega = false;
            ChkFactura = false;
        }

        private void LimpiarPantalla()
        {
            TxtNPrimario = "";
            TxtNSecundario = "";
            TxtPaterno = "";
            TxtMaterno = "";
            TxtNumTelefono = "";
            TxtRazonSocial = "";
            TxtRFC = "";
            TxtEmail = "";
            ListaTelefonos.Clear();
            LimpiarDireccion();
            ListaDirecciones.Clear();
        }

        private bool ErrorDatosDireccion()
        {
            bool existeError = false;
            if (string.IsNullOrWhiteSpace(TxtCalle1) == true)
                existeError = true;
            else if (string.IsNullOrWhiteSpace(TxtExterior) == true)
                existeError = true;
            else if (string.IsNullOrWhiteSpace(TxtColonia) == true)
                existeError = true;
            else if (string.IsNullOrWhiteSpace(TxtCPostal) == true)
                existeError = true;
            else if (Municipio == null)
                existeError = true;
            return existeError;
        }

        private void AgregarTelefono(object parameter)
        {
            if (TipoTel != null)
            {
                vw_lista_telefonos consulta = ListaTelefonos.SingleOrDefault(x => x.numtelefono == TxtNumTelefono);
                if (consulta == null && string.IsNullOrWhiteSpace(TxtNumTelefono) == false)
                {
                    DatosTel = new vw_lista_telefonos
                    {
                        idlstipotelefono = TipoTel.idlsselecciondetalle,
                        idtelefono = Convert.ToString(Guid.NewGuid()),
                        numtelefono = TxtNumTelefono,
                        idpersona = IdPersona,
                        descripcion = TipoTel.descripcion
                    };
                    ListaTelefonos.Add(DatosTel);
                    using (var bd = new bd_guajiroEntities())
                    {
                        var tel = new tbl_telefonos
                        {
                            idtelefono = DatosTel.idtelefono,
                            idlstipotelefono = DatosTel.idlstipotelefono,
                            idpersona = IdPersona,
                            numtelefono = DatosTel.numtelefono
                        };
                        bd.tbl_telefonos.Add(tel);
                        int c = bd.SaveChanges();
                        if (c > 0)
                        {
                            TxtMensaje = "Los datos telefónicos han sido agregados correctamente.";
                            VerMensaje = true;
                        }
                    }
                    TxtNumTelefono = "";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(TxtNumTelefono) == true)
                        TxtMensaje = "Debe ingresar un número telefónico.";
                    else
                        TxtMensaje = "El número telefónico ingresado ya existe en la lista.";
                    VerMensaje = true;
                }
            }
            else
            {
                TxtMensaje = "Para agregar un teléfono a la lista debes elegir un tipo e ingresar un número telefónico.";
                VerMensaje = true;
            }
        }

        private void BorrarTelefono(object parameter)
        {
            string idtel = parameter as string;
            var infoTel = GuajiroEF.tbl_telefonos.SingleOrDefault(x => x.idtelefono == idtel);
            if (infoTel != null)
            {
                using(var bd = new bd_guajiroEntities())
                {
                    bd.Entry(infoTel).State = EntityState.Deleted;
                    int b = bd.SaveChanges();
                }
            }
            vw_lista_telefonos consulta = ListaTelefonos.SingleOrDefault(x => x.idtelefono == idtel);
            if (consulta != null)
                ListaTelefonos.Remove(consulta);
        }

        private void FiltrarMunicipios(string idestado)
        {
            if (idestado != null)
            {
                List<tbl_municipios> lista = GuajiroEF.tbl_municipios.Where(x => x.idestado == idestado).ToList();
                ListaMunicipios = new ObservableCollection<tbl_municipios>(lista);
            }
        }

        private void CerrarMensaje(object parameter) => VerMensaje = false;

        private bool ErrorDatosPersonales(bool esPFisica)
        {
            bool existeError = false;
            if(esPFisica == true)
            {
                existeError = string.IsNullOrWhiteSpace(TxtNPrimario);
                existeError = string.IsNullOrWhiteSpace(TxtPaterno);
            }
            else
            {
                existeError = string.IsNullOrWhiteSpace(TxtRazonSocial);
            }
            return existeError;
        }

        private void GuardarCliente(object parameter)
        {
            if (string.IsNullOrEmpty(IdPersona) == false)
            {
                EditarCliente();
            }
            else
            {
                string razsoc = string.Empty;
                bool checkPersona = false;
                if (TipoPersona != "e0e8f331-fe83-11e7-83f1-204747335338") //Persona Física
                {
                    razsoc = TxtNPrimario + " " + TxtNSecundario + " " + TxtPaterno + " " + TxtMaterno;
                    checkPersona = ErrorDatosPersonales(true);
                }
                else
                {
                    razsoc = TxtRazonSocial;
                    checkPersona = ErrorDatosPersonales(false);
                }
                if (checkPersona != true && ListaDirecciones.Count > 0)
                {
                    int guardados = 0;
                    using (var bd = new bd_guajiroEntities())
                    {
                        tbl_personas persona = new tbl_personas
                        {
                            idpersona = IdPersona,//Convert.ToString(Guid.NewGuid()),
                            idlstipopersona = "e47c6009-368a-11e7-b904-204747335338", //Tipo Cliente
                            idlstipocontribuyente = TipoPersona,
                            nprimario = TxtNPrimario,
                            nsecundario = TxtNSecundario,
                            paterno = TxtPaterno,
                            materno = TxtMaterno,
                            razon_social = razsoc,
                            rfc = TxtRFC,
                            email = TxtEmail,
                            crea_usuario = CreaUsuario
                        };
                        bd.tbl_personas.Add(persona);
                        guardados = bd.SaveChanges();
                        if (guardados > 0)
                        {
                            TxtMensaje = "Los datos del Cliente han sido guardados correctamente";
                            VerMensaje = true;
                            LimpiarPantalla();
                        }
                    }
                }
                else
                {
                    TxtMensaje = "Existe un error que no permite guardar los datos. Verifique que los campos obligatorios contengan información.";
                    VerMensaje = true;
                }
            }
        }

        private void EditarCliente()
        {
            int editado = 0;
            using (var bd = new bd_guajiroEntities())
            {
                tbl_personas editarPersona = bd.tbl_personas.Single(x => x.idpersona == IdPersona);
                editarPersona.idlstipocontribuyente = TipoPersona;
                editarPersona.nprimario = TxtNPrimario;
                editarPersona.nsecundario = TxtNSecundario;
                editarPersona.paterno = TxtPaterno;
                editarPersona.materno = TxtMaterno;
                string razsoc = string.Empty;
                bool checkPersona = false;
                if (TipoPersona != "e0e8f331-fe83-11e7-83f1-204747335338") //Persona Física
                {
                    razsoc = TxtNPrimario + " " + TxtNSecundario + " " + TxtPaterno + " " + TxtMaterno;
                    checkPersona = ErrorDatosPersonales(true);
                }
                else
                {
                    razsoc = TxtRazonSocial;
                    checkPersona = ErrorDatosPersonales(false);
                }
                editarPersona.razon_social = razsoc;
                editarPersona.rfc = TxtRFC;
                editarPersona.email = TxtEmail;
                bd.Entry(editarPersona).State = EntityState.Modified;
                editado = bd.SaveChanges();
            };
        }

        #endregion
    }
}
