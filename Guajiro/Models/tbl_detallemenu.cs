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
    
    public partial class tbl_detallemenu
    {
        public string iddetalle { get; set; }
        public string idmenu { get; set; }
        public string iditem { get; set; }
    
        public virtual tbl_menudeldia tbl_menudeldia { get; set; }
        public virtual tbl_items tbl_items { get; set; }
    }
}
