
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
    
public partial class PROYECTO
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public PROYECTO()
    {

        this.COSTO_PROYECTOS = new HashSet<COSTO_PROYECTOS>();

    }


    public string CODIGO_PROYECTO { get; set; }

    public string NOMBRE_PROYECTO { get; set; }

    public string FK_CODIGO_CATEGORIA { get; set; }

    public Nullable<System.DateTime> FECHA_INICIO { get; set; }

    public string ESTADO { get; set; }

    public Nullable<System.DateTime> FECHA_FIN { get; set; }

    public Nullable<decimal> PRECIO { get; set; }

    public string COMENTARIO { get; set; }



    public virtual CATEGORIA_PROYECTOS CATEGORIA_PROYECTOS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<COSTO_PROYECTOS> COSTO_PROYECTOS { get; set; }

}

}
