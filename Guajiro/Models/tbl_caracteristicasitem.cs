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
    
    public partial class tbl_caracteristicasitem
    {
        public string idcaracteristica { get; set; }
        public string iditem { get; set; }
        public string idlsunidadmedida { get; set; }
        public string idlstipocaracteristica { get; set; }
        public Nullable<decimal> valor { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
        public string crea_usuario { get; set; }
    }
}
