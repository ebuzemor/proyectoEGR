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
    
    public partial class tbl_corte_mov
    {
        public int idcortemov { get; set; }
        public string idcorte { get; set; }
        public string idmovimiento { get; set; }
    
        public virtual tbl_corte tbl_corte { get; set; }
        public virtual tbl_movimientos tbl_movimientos { get; set; }
    }
}
