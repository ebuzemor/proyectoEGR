using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace Guajiro.Common
{
    public class MenuOpciones : Notifier
    {
        #region Variables

        private String _nombre;
        private Object _contenido;
        private ScrollBarVisibility _horizontalScrollBarVisibilidad;
        private ScrollBarVisibility _verticalScrollBarVisibilidad;
        private Thickness _margenRequerimiento = new Thickness(16);

        public String Nombre
        {
            get => _nombre;
            //set => SetProperty(ref _nombre, value);
            set => this.MutateVerbose(ref _nombre, value, RaisePropertyChanged());
        }

        public Object Contenido
        {
            get => _contenido;
            //set => SetProperty(ref _contenido, value);
            set => this.MutateVerbose(ref _contenido, value, RaisePropertyChanged());
        }

        public ScrollBarVisibility HorizontalScrollBarVisibilidad
        {
            get => _horizontalScrollBarVisibilidad;
            //set => SetProperty(ref _horizontalScrollBarVisibilidad, value);
            set => this.MutateVerbose(ref _horizontalScrollBarVisibilidad, value, RaisePropertyChanged());
        }

        public ScrollBarVisibility VerticalScrollBarVisibilidad
        {
            get => _verticalScrollBarVisibilidad;
            //set => SetProperty(ref _verticalScrollBarVisibilidad, value);
            set => this.MutateVerbose(ref _verticalScrollBarVisibilidad, value, RaisePropertyChanged());
        }

        public Thickness MargenRequerimiento
        {
            get => _margenRequerimiento;
            //set => SetProperty(ref _margenRequerimiento, value);
            set => this.MutateVerbose(ref _margenRequerimiento, value, RaisePropertyChanged());
        }

        #endregion

        #region Constructor

        public MenuOpciones(String nombre, Object contenido)
        {
            _nombre = nombre;
            _contenido = contenido;
        }

        #endregion

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged1;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged1?.Invoke(this, args);
        }

        #endregion
    }
}
