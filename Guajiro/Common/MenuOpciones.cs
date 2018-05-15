﻿using System;
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
        private string _icono;
        private ScrollBarVisibility _horizontalScrollBarVisibilidad;
        private ScrollBarVisibility _verticalScrollBarVisibilidad;
        private Thickness _margenRequerimiento = new Thickness(16);

        public String Nombre
        {
            get => _nombre;
            set => this.MutateVerbose(ref _nombre, value, RaisePropertyChanged());
        }

        public Object Contenido
        {
            get => _contenido;
            set => this.MutateVerbose(ref _contenido, value, RaisePropertyChanged());
        }

        public string Icono
        {
            get => _icono;
            set => this.MutateVerbose(ref _icono, value, RaisePropertyChanged());
        }

        public ScrollBarVisibility HorizontalScrollBarVisibilidad
        {
            get => _horizontalScrollBarVisibilidad;
            set => this.MutateVerbose(ref _horizontalScrollBarVisibilidad, value, RaisePropertyChanged());
        }

        public ScrollBarVisibility VerticalScrollBarVisibilidad
        {
            get => _verticalScrollBarVisibilidad;
            set => this.MutateVerbose(ref _verticalScrollBarVisibilidad, value, RaisePropertyChanged());
        }

        public Thickness MargenRequerimiento
        {
            get => _margenRequerimiento;
            set => this.MutateVerbose(ref _margenRequerimiento, value, RaisePropertyChanged());
        }

        #endregion

        #region Constructor

        public MenuOpciones(string icono, String nombre, Object contenido)
        {
            _icono = icono;
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
