using System;
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
        #region Variables

        private tbl_usuarios _usuarioActual;
        private Boolean _esValido;
        private String _txtLogin;
        private String _txtPassword;

        public tbl_usuarios UsuarioActual { get => _usuarioActual; set => SetProperty(ref _usuarioActual, value); }
        public bool EsValido { get => _esValido; set => SetProperty(ref _esValido, value); }
        public string TxtLogin { get => _txtLogin; set => SetProperty(ref _txtLogin, value); }
        public string TxtPassword { get => _txtPassword; set => SetProperty(ref _txtPassword, value); }

        public RelayCommand ValidarUsuarioCommand { get; set; }
        public bd_guajiroEntities guajiroEF;

        #endregion

        #region Constructor

        public LoginViewModel()
        {
            //guajiroEF = new bd_guajiroEntities();
            ValidarUsuarioCommand = new RelayCommand(ValidarUsuario);
            TxtLogin = "admin";
            //TxtPassword = "admin";
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
        }

        #endregion
    }
}
