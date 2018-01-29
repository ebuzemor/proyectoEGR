using Guajiro.Common;
using Guajiro.Models;
using System.Collections.ObjectModel;

namespace Guajiro.ViewModels
{
    public class DetallesMenuDiaViewModel : Notifier
    {
        #region Commands

        #endregion

        #region Variables
        private ObservableCollection<vw_detallemenu> _listaDetalles;

        public ObservableCollection<vw_detallemenu> ListaDetalles { get => _listaDetalles; set { _listaDetalles = value; OnPropertyChanged("ListaDetalles"); } }
        #endregion

        #region Constructor
        public DetallesMenuDiaViewModel() { }
        #endregion

        #region Métodos

        #endregion
    }
}
