
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DISETOP.Models
{

using System;
    using System.Collections.Generic;
    
public partial class PAGO
{

    public string CODIGO_PAGO { get; set; }

    public string FK_CODIGO_EMPLEADO { get; set; }

    public string CUENTA_BANCARIA { get; set; }

    public string DIAS_A_PAGAR { get; set; }

    public Nullable<decimal> MONTO { get; set; }

    public Nullable<System.DateTime> FECHA_DE_PAGO { get; set; }

    public string COMENTARIO { get; set; }



    public virtual EMPLEADO EMPLEADO { get; set; }

}

}
