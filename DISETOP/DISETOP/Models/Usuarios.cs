using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DISETOP.Models
{
    public class Usuarios
    {
        public int id_Usuario { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }


        public string ConfirmarClave { get; set; }

    }
}