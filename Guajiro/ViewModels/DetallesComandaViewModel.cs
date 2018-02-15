using Guajiro.Common;
using Guajiro.Models;
using System.Collections.ObjectModel;

namespace Guajiro.ViewModels
{
    public class DetallesComandaViewModel : Notifier
    {
        #region Variables
        private ObservableCollection<tbl_detallescomanda> _listaDetalles;

        public ObservableCollection<tbl_detallescomanda> ListaDetalles { get => _listaDetalles; set { _listaDetalles = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public DetallesComandaViewModel(){ }
        #endregion

        #region Métodos

        #endregion
    }
}
