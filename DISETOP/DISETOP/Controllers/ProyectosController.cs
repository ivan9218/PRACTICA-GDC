using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DISETOP.Models;
using System.Data.Entity;
using System.Web.UI;
using Microsoft.Ajax.Utilities;

namespace DISETOP.Controllers
{
    public class ProyectosController : Controller
    {

        private DISETOPEntities db = new DISETOPEntities();
        // GET: Proyectos


        //***********INICIO DE LISTAR TOTAL CON FORANEA EN GRID Y MODALS*************
        public ActionResult Index()
        {
            return View();
        }
        public List<PROYECTO> getProyectos()
        {
            List<PROYECTO> proyectos = new List<PROYECTO>();
            using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_RetornaProyectos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PROYECTO proyecto = new PROYECTO
                            {
                                CODIGO_PROYECTO = reader["CODIGO_PROYECTO"].ToString(),
                                NOMBRE_PROYECTO = reader["NOMBRE_PROYECTO"].ToString(),
                                FK_CODIGO_CATEGORIA = reader["FK_CODIGO_CATEGORIA"].ToString(),
                                ESTADO = reader["ESTADO"].ToString(),
                                COMENTARIO = reader["COMENTARIO"].ToString()
                            };

                            // Añadir manejo de excepciones para MONTO y FECHA DE PAGO
                            try
                            {
                                proyecto.PRECIO = Convert.ToDecimal(reader["PRECIO"]);
                                proyecto.FECHA_INICIO = Convert.ToDateTime(reader["FECHA_INICIO"]);
                                proyecto.FECHA_FIN= Convert.ToDateTime(reader["FECHA_FIN"]);
                            }
                            catch (Exception)
                            {
                                // Manejar excepciones
                            }

                            // Obtener el nombre del empleado a partir del código de empleado
                            var codigoCategoria = proyecto.FK_CODIGO_CATEGORIA; // El código de empleado es una cadena (varchar)
                            var categoria = db.CATEGORIA_PROYECTOS.FirstOrDefault(e => e.CODIGO_CATEGORIA == codigoCategoria);

                            if (categoria != null)
                            {
                                proyecto.NOMBRE_CATEGORIA = categoria.NOMBRE_CATEGORIA;
                            }


                            proyectos.Add(proyecto);
                        }
                    }
                }
            }

            return proyectos;
        }
        // ESTE ES EL QUE CARGA LOS DATOS DE EMPLEADO OSEA DE LA FORANEA EN EL MODAL DE INSERTAR
        //ESTA REPETIDO HAY QUE LLAMARLO DE DONDE ESTA 
        public List<CATEGORIA_PROYECTOS> getCategorias()
        {
            List<CATEGORIA_PROYECTOS> cateoria_proyectos = new List<CATEGORIA_PROYECTOS>();

            using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_RetornaCategoria_Proyectos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CATEGORIA_PROYECTOS cateoria_proyecto = new CATEGORIA_PROYECTOS
                            {
                                CODIGO_CATEGORIA = reader["CODIGO_CATEGORIA"].ToString(),
                                NOMBRE_CATEGORIA = reader["NOMBRE_CATEGORIA"].ToString(),
                                COMENTARIO = reader["COMENTARIO"].ToString()
                            };

                            cateoria_proyectos.Add(cateoria_proyecto);
                        }
                    }
                }
            }

            return cateoria_proyectos;
        }


        //INICIO LISTAR EN EL MODAL
        public ActionResult VistaProyectos()
        {
            List<PROYECTO> proyectos= getProyectos();
            List<CATEGORIA_PROYECTOS> categorias = getCategorias();
            var model = new Proyectos_Y_CategoriasModel
            {
                Proyectos = proyectos,
                Categorias = categorias
            };


            return View(model);
        }
        // FIN DE LISTAR EN EL MODAL 

        // Método para obtener el nombre del empleado ESTE ES PARA LISTAR EN EL GRID LO QUE YO OCUPE BASADO EN EL ID DE LA LLAVE FORANEA
        [HttpGet] // Puedes usar [HttpGet] si corresponde
        public ActionResult ObtenerNombreCategoria(string codigoCategoria)
        {
            var cateoria_proyecto = db.CATEGORIA_PROYECTOS.FirstOrDefault(e => e.CODIGO_CATEGORIA == codigoCategoria);

            if (cateoria_proyecto != null)
            {
                return Json(cateoria_proyecto.NOMBRE_CATEGORIA, JsonRequestBehavior.AllowGet);
            }

            // Manejar el caso donde no se encuentra el empleado
            return Json("No se encontró la Categoría de Proyecto", JsonRequestBehavior.AllowGet);
        }

        // FIN LISTAR JALANDO NOMBRE EMPLEADO A PARTIR DE LLAVE FORANEA 

        //***********FIN DE LISTAR TOTAL CON FORANEA EN GRID Y MODALS*************





        //INICIO DE INSERTAR EN GRID
        [HttpGet]
        public ActionResult InsertarProyecto()
        {
            // Aquí deberías cargar los datos necesarios para el formulario de inserción de proyecto, si es necesario.
            return View();
        }

        [HttpPost]
        public ActionResult InsertarProyecto(PROYECTO proyecto)
        {
       
            if (ModelState.IsValid)
            {

                //INICIO VALIDACIONES
             
                // Verifica si el MONTO no es nulo y está fuera del rango permitido
                if (proyecto.PRECIO != null)
                {
                    if (proyecto.PRECIO < 0 || proyecto.PRECIO > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Precio' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    proyecto.PRECIO = 0;
                }

                // Asigna cadena vacía a COMENTARIO si está nulo
                if (string.IsNullOrWhiteSpace(proyecto.COMENTARIO))
                {
                    proyecto.COMENTARIO = string.Empty;
                }
                // Asigna cadena vacía a ESTADO si está nulo
                if (string.IsNullOrWhiteSpace(proyecto.ESTADO))
                {
                    proyecto.ESTADO = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(proyecto.CODIGO_PROYECTO))
                {
                    return Json(new { success = false, message = "El campo 'Código de Proyecto' es obligatorio." });
                }
                if (string.IsNullOrWhiteSpace(proyecto.NOMBRE_PROYECTO))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Proyecto' es obligatorio." });
                }
                if (string.IsNullOrWhiteSpace(proyecto.FK_CODIGO_CATEGORIA))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Categoría' es obligatorio." });
                }

                if (proyecto.FECHA_INICIO != null)
                {
                    proyecto.FECHA_INICIO = proyecto.FECHA_INICIO.Value.Date; // Elimina la parte de la hora y los minutos
                }

                if (proyecto.FECHA_FIN != null)
                {
                    proyecto.FECHA_FIN = proyecto.FECHA_FIN.Value.Date; // Elimina la parte de la hora y los minutos
                }

              

                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    var proyectoExistente = context.PROYECTOS.FirstOrDefault(p => p.CODIGO_PROYECTO == proyecto.CODIGO_PROYECTO);

                    if (proyectoExistente != null)
                    {
                        return Json(new { success = false, message = "El proyecto con el código " + proyecto.CODIGO_PROYECTO + " ya existe." });
                    }

                    //FIN VALIDACIONES
                    context.Database.ExecuteSqlCommand("sp_InsertarProyecto @CodigoProyecto, @NombreProyecto, @FK_CodigoCategoria, @FechaInicio, @Estado, @FechaFin, @Precio, @Comentario",
                        new SqlParameter("@CodigoProyecto", proyecto.CODIGO_PROYECTO),
                        new SqlParameter("@NombreProyecto", proyecto.NOMBRE_PROYECTO),
                        new SqlParameter("@FK_CodigoCategoria", proyecto.FK_CODIGO_CATEGORIA),
                        new SqlParameter("@FechaInicio", proyecto.FECHA_INICIO.HasValue ? (object)proyecto.FECHA_INICIO.Value : DBNull.Value),
                        new SqlParameter("@Estado", proyecto.ESTADO),
                        new SqlParameter("@FechaFin", proyecto.FECHA_FIN.HasValue ? (object)proyecto.FECHA_FIN.Value : DBNull.Value),
                        new SqlParameter("@Precio", proyecto.PRECIO), // Utiliza la fecha formateada
                        new SqlParameter("@Comentario", proyecto.COMENTARIO)
                    );
                }

                return Json(new { success = true });
            }

           
            return Json(new { success = false, message = "Error al agregar el proyecto." });
        }
        //FIN DE INSERTAR EN GRID


        //***************************************************************************************************************************************
        //INICIO DE EDITAR EN GRID
        [HttpGet]
        public ActionResult EditarProyecto(string codigo_proyecto)
        {
            // Recuperar el pago de la base de datos usando el codigo_pago proporcionado
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                PROYECTO proyecto = context.PROYECTOS.FirstOrDefault(e => e.CODIGO_PROYECTO == codigo_proyecto);

                if (proyecto != null)
                {
                    return View(proyecto); // Mostrar la vista de edición con los datos del proyecto
                }
                else
                {
                    return HttpNotFound(); // Devolver un error 404 si no se encuentra el proyecto
                }
            }
        }

        [HttpPost]
        public ActionResult EditarProyecto(PROYECTO proyecto)
        {

            if (ModelState.IsValid)
            {

                //INICIO VALIDACIONES

                // Verifica si el MONTO no es nulo y está fuera del rango permitido
                if (proyecto.PRECIO != null)
                {
                    if (proyecto.PRECIO < 0 || proyecto.PRECIO > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Precio' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    proyecto.PRECIO = 0;
                }

                // Asigna cadena vacía a COMENTARIO si está nulo
                if (string.IsNullOrWhiteSpace(proyecto.COMENTARIO))
                {
                    proyecto.COMENTARIO = string.Empty;
                }
                // Asigna cadena vacía a ESTADO si está nulo
                if (string.IsNullOrWhiteSpace(proyecto.ESTADO))
                {
                    proyecto.ESTADO = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(proyecto.CODIGO_PROYECTO))
                {
                    return Json(new { success = false, message = "El campo 'Código de Proyecto' es obligatorio." });
                }
                if (string.IsNullOrWhiteSpace(proyecto.NOMBRE_PROYECTO))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Proyecto' es obligatorio." });
                }

                if (proyecto.FECHA_INICIO != null)
                {
                    proyecto.FECHA_INICIO = proyecto.FECHA_INICIO.Value.Date; // Elimina la parte de la hora y los minutos
                }

                if (proyecto.FECHA_FIN != null)
                {
                    proyecto.FECHA_FIN = proyecto.FECHA_FIN.Value.Date; // Elimina la parte de la hora y los minutos
                }
                if (string.IsNullOrWhiteSpace(proyecto.FK_CODIGO_CATEGORIA))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Categoría' es obligatorio." });
                }


                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    //var proyectoExistente = context.PROYECTOS.FirstOrDefault(p => p.CODIGO_PROYECTO == proyecto.CODIGO_PROYECTO);

                    //if (proyectoExistente != null)
                    //{
                    //    return Json(new { success = false, message = "El proyecto con el código " + proyecto.CODIGO_PROYECTO + " ya existe." });
                    //}

                    //FIN VALIDACIONES
                    context.Database.ExecuteSqlCommand("sp_EditarProyectos @CodigoProyecto, @NombreProyecto, @FK_CodigoCategoria, @FechaInicio, @Estado, @FechaFin, @Precio, @Comentario",
                        new SqlParameter("@CodigoProyecto", proyecto.CODIGO_PROYECTO),
                        new SqlParameter("@NombreProyecto", proyecto.NOMBRE_PROYECTO),
                        new SqlParameter("@FK_CodigoCategoria", proyecto.FK_CODIGO_CATEGORIA),
                        new SqlParameter("@FechaInicio", proyecto.FECHA_INICIO.HasValue ? (object)proyecto.FECHA_INICIO.Value : DBNull.Value),
                        new SqlParameter("@Estado", proyecto.ESTADO),
                        new SqlParameter("@FechaFin", proyecto.FECHA_FIN.HasValue ? (object)proyecto.FECHA_FIN.Value : DBNull.Value),
                        new SqlParameter("@Precio", proyecto.PRECIO), // Utiliza la fecha formateada
                        new SqlParameter("@Comentario", proyecto.COMENTARIO)
                    );
                }

                return Json(new { success = true });
            }


            return Json(new { success = false, message = "Error al actualizar el proyecto." });
        }
        //FIN DE EDITAR EN GRID



        //INICIO ELIMINAR EN GRID
        public ActionResult EliminarProyecto(string CodigoProyecto)
        {
            // Llama al procedimiento almacenado para eliminar el pago
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                context.Database.ExecuteSqlCommand("exec sp_EliminaProyectos @CodigoProyecto", new SqlParameter("@CodigoProyecto", CodigoProyecto));
            }


            return RedirectToAction("VistaProyectos");
        }


        //FIN ELIMINAR EN GRID










    }
}