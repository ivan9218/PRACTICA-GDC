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


    // AQUI ES DONDE SE VAN A PONER LAS LISTAS PARA PODER LLAMAR LAS LLAVES FORANEAS 
    // EN CLASES CON FORANEAS SE NECESITA PONER AQUI TAMBIEN EL DE LISTAR EN EL GRID PARA HACER 1 SOLA INVOCACION 
    public class PagosEmpleadosModel
    {
        public List<PAGO> Pagos { get; set; }
        public List<EMPLEADO> Empleados { get; set; }
    }

    public class Proyectos_Y_CategoriasModel
    {
        public List<PROYECTO> Proyectos { get; set; }
        public List<CATEGORIA_PROYECTOS> Categorias { get; set; }
    }
}