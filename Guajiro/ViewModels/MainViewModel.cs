using System;
using Guajiro.Common;

namespace Guajiro.ViewModels
{
    public class MainViewModel : Notifier
    {
        #region Variables

        private String _titulo;

        public string Titulo { get => _titulo; set { _titulo = value; OnPropertyChanged(); } }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            Titulo = "Restaurante El Guajiro";
        }

        #endregion
    }
}
