//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Guajiro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_lista_comandas
    {
        public string idcomanda { get; set; }
        public string razon_social { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public int num_comanda { get; set; }
        public Nullable<bool> para_llevar { get; set; }
        public string desc_mesa { get; set; }
        public Nullable<decimal> total { get; set; }
    }
}
