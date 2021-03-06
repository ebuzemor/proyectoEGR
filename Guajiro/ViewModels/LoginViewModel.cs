﻿using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Guajiro.Common;
using Guajiro.Models;
using Guajiro.Views;
using System.Windows.Controls;

namespace Guajiro.ViewModels
{
    public class LoginViewModel : Notifier
    {
        #region Commands
        public RelayCommand ValidarUsuarioCommand { get; set; }
        public RelayCommand CerrarMensajeCommand { get; set; }
        #endregion

        #region Variables

        private tbl_usuarios _usuarioActual;
        private bool _esValido;
        private string _txtLogin;
        private string _txtPassword;
        private bool _verMensaje;
        private string _txtMensaje;

        public tbl_usuarios UsuarioActual { get => _usuarioActual; set { _usuarioActual = value; OnPropertyChanged(); } }
        public bool EsValido { get => _esValido; set { _esValido = value; OnPropertyChanged(); } }
        public string TxtLogin { get => _txtLogin; set { _txtLogin = value; OnPropertyChanged(); } }
        public string TxtPassword { get => _txtPassword; set { _txtPassword = value; OnPropertyChanged(); } }
        public bool VerMensaje { get => _verMensaje; set { _verMensaje = value; OnPropertyChanged(); } }
        public string TxtMensaje { get => _txtMensaje; set { _txtMensaje = value; OnPropertyChanged(); } }
        public bd_guajiroEntities guajiroEF;
        #endregion

        #region Constructor

        public LoginViewModel()
        {
            guajiroEF = new bd_guajiroEntities();
            ValidarUsuarioCommand = new RelayCommand(ValidarUsuario);
            CerrarMensajeCommand = new RelayCommand(CerrarMensaje);
        }

        #endregion

        #region Metodos

        private string GenerarMD5(string psw)
        {
            if (String.IsNullOrEmpty(psw))
                return string.Empty;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] clv = Encoding.Default.GetBytes(psw);
            byte[] res = md5.ComputeHash(clv);
            return BitConverter.ToString(res);
        }

        private void ValidarCredenciales(String login, String password)
        {
            String psw = GenerarMD5(password);
            psw = psw.Replace("-", "");
            UsuarioActual = guajiroEF.tbl_usuarios.SingleOrDefault(x => x.login.Equals(login) && x.password.Equals(psw.ToLower()));
            EsValido = (UsuarioActual != null) ? true : false;
        }

        private void ValidarUsuario(object parameter)
        {
            PasswordBox pwbox = parameter as PasswordBox;
            TxtPassword = pwbox.Password;
            ValidarCredenciales(TxtLogin, TxtPassword);
            if (EsValido == true)
            {
                PrincipalViewModel vmPrincipal = new PrincipalViewModel(UsuarioActual);
                PrincipalView vwPrincipal = new PrincipalView
                {
                    DataContext = vmPrincipal
                };
                Navigator.NavigationService.Navigate(vwPrincipal);
            }
            else
            {
                TxtMensaje = "El usuario y/o contraseña son incorrectos";
                VerMensaje = true;
            }
        }

        private void CerrarMensaje(object parameter) => VerMensaje = false;

        #endregion
    }
}