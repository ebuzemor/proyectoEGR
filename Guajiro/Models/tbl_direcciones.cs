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
    
    public partial class tbl_direcciones
    {
        public string iddireccion { get; set; }
        public string idpersona { get; set; }
        public string idmunicipio { get; set; }
        public string calle1 { get; set; }
        public string calle2 { get; set; }
        public string interior { get; set; }
        public string exterior { get; set; }
        public string colonia { get; set; }
        public string codigopostal { get; set; }
        public Nullable<bool> entrega { get; set; }
        public Nullable<bool> fiscal { get; set; }
    }
}
