//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DISETOP.Models
{
    using System;
    
    public partial class sp_RetornaActivos_Result
    {
        public string CODIGO_ACTIVO { get; set; }
        public string NOMBRE_DE_ACTIVO { get; set; }
        public string NUMERO_DE_SERIE { get; set; }
        public Nullable<System.DateTime> FECHA_DE_COMPRA { get; set; }
        public string PROVEEDOR { get; set; }
        public Nullable<decimal> VALOR_DE_COMPRA { get; set; }
        public Nullable<decimal> VALOR_ACTUAL { get; set; }
        public string COMENTARIO { get; set; }
    }
}
