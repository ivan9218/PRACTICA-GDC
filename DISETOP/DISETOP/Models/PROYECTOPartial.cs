using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace DISETOP.Models
{  /// <summary>
/// CLASE PARCIAL CREADA COMO EXTENSION PARA PODER USAR ELEMENTOS ADICIONALES, POR EJEMPLO AQUI 
/// ES PARA MOSTAR EL NONBRE DE EMPLEADO EN EL SELECT QUE RECIBE POR DENAJO EL CODIGO DE EMPLEADO
/// </summary>
    [NotMapped]
    public partial class PROYECTO
    {
        public string NOMBRE_CATEGORIA { get; set; }
    }
}