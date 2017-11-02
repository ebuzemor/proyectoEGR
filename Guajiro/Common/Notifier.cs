using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Guajiro.Common
{
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            //if (handler != null)
            //    handler(this, new PropertyChangedEventArgs(propertyName));
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));//Esto es lo mismo que el if anterior.
        }

        public void SetProperty<T>(ref T privateField, T newValue, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(privateField, newValue))
                return;
            privateField = newValue;
            OnPropertyChanged();
        }
    }
}
