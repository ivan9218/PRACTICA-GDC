using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DISETOP.Models;
using System.Data.Entity;

namespace DISETOP.Controllers
{
    public class Categoria_ProyectosController : Controller
    {

        private DISETOPEntities db = new DISETOPEntities();
        // GET: Categoria_Proyectos
        public ActionResult Index()
        {
            return View();
        }

        //INICIO DE LISTAR EN GRID
        public ActionResult VistaCategoria_Proyectos()
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

                            // Añadir manejo de excepciones para CEDULA y TELEFONO
                            //try
                            //{
                            //    empleado.CEDULA = Convert.ToInt32(reader["CEDULA"]);
                            //    empleado.TELEFONO = Convert.ToInt32(reader["TELEFONO"]);
                            //}
                            //catch (Exception)
                            //{

                            //}

                            cateoria_proyectos.Add(cateoria_proyecto);
                        }
                    }
                }
            }

            return View(cateoria_proyectos);
        }


        //FIN DE LISTAR EN GRID

        //INICIO DE INSERTAR EN GRID

        [HttpGet]
        public ActionResult InsertarCategoria_Proyectos()
        {
            return View(); // Esta vista mostrará el formulario de inserción
        }

        [HttpPost]
        public ActionResult InsertarCategoria_Proyectos(CATEGORIA_PROYECTOS categoria_proyectos)
        {
            // INICIO VALIDACIONES
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(categoria_proyectos.CODIGO_CATEGORIA))
                {
                    return Json(new { success = false, message = "El campo 'Código de Categoría' es obligatorio." });
                }
                if (string.IsNullOrWhiteSpace(categoria_proyectos.NOMBRE_CATEGORIA))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Categoría' es obligatorio." });
                }
                // Asigna cadena vacía a comentario si está nulo
                if (string.IsNullOrWhiteSpace(categoria_proyectos.COMENTARIO))
                {
                     categoria_proyectos.COMENTARIO = string.Empty;
                }

                // Inserta la nueva categoria de proyectos en la base de datos utilizando el procedimiento almacenado
                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    var categoriaExistente = context.CATEGORIA_PROYECTOS.FirstOrDefault(e => e.CODIGO_CATEGORIA == categoria_proyectos.CODIGO_CATEGORIA);

                    if (categoriaExistente != null)
                    {
                        return Json(new { success = false, message = "La categoría de proyectos con el código " + categoria_proyectos.CODIGO_CATEGORIA + " ya existe." });
                    }

                    var categoriaExistentePorNombre = context.CATEGORIA_PROYECTOS.FirstOrDefault(e => e.NOMBRE_CATEGORIA == categoria_proyectos.NOMBRE_CATEGORIA);

                    if (categoriaExistentePorNombre != null)
                    {
                        return Json(new { success = false, message = "La categoria de proyectos con el nombre " + categoria_proyectos.NOMBRE_CATEGORIA + " ya existe." });
                    }
                    // FIN VALIDACIONES

                    context.Database.ExecuteSqlCommand("sp_InsertaCategoria_Proyectos @codigo_categoria, @nombre_categoria, @comentario",
                        new SqlParameter("@codigo_categoria", categoria_proyectos.CODIGO_CATEGORIA),
                        new SqlParameter("@nombre_categoria", categoria_proyectos.NOMBRE_CATEGORIA),
                        new SqlParameter("@comentario", categoria_proyectos.COMENTARIO)
                    );
                }

                return Json(new { success = true });
            }

            return View(categoria_proyectos);
        }


        //FIN DE INSERTAR EN GRID



        //INICIO DE EDITAR EN GRID

        [HttpGet]
        public ActionResult EditarCategoria_Proyectos(string codigo_categoria)
        {
            // Recuperar la categoria de la base de datos usando el código_categoria proporcionado
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                CATEGORIA_PROYECTOS categoria_proyectos = context.CATEGORIA_PROYECTOS.FirstOrDefault(e => e.CODIGO_CATEGORIA == codigo_categoria);

                if (categoria_proyectos != null)
                {
                    return View(categoria_proyectos); // Mostrar la vista de edición con los datos de la categoria de proyectos
                }
                else
                {
                    return HttpNotFound(); // Devolver un error 404 si no se encuentra la categoria de proyectos
                }
            }
        }



        [HttpPost]
        public ActionResult EditarCategoria_Proyectos(CATEGORIA_PROYECTOS categoria_proyectos)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    // INICIO VALIDACIONES
                    // Verifica si el nombre de categoria de proyectos ya existe en otra categoria de proyectos   (excluyendo al empleado actual)
                    var categoria_proyectosExistente = context.CATEGORIA_PROYECTOS.FirstOrDefault(e => e.NOMBRE_CATEGORIA == categoria_proyectos.NOMBRE_CATEGORIA && e.CODIGO_CATEGORIA != categoria_proyectos.CODIGO_CATEGORIA);

                    if (categoria_proyectosExistente != null)
                    {
                        return Json(new { success = false, message = "Ya existe otra categoría de proyectos con el mismo nombre." });
                    }

                    if (string.IsNullOrWhiteSpace(categoria_proyectos.CODIGO_CATEGORIA))
                    {
                        return Json(new { success = false, message = "El campo 'Código de Categoría' es obligatorio." });
                    }
                    if (string.IsNullOrWhiteSpace(categoria_proyectos.NOMBRE_CATEGORIA))
                    {
                        return Json(new { success = false, message = "El campo 'Nombre de Categoría' es obligatorio." });
                    }
               
                    // Asigna cadena vacía a comentario si está nulo
                    if (string.IsNullOrWhiteSpace(categoria_proyectos.COMENTARIO))
                    {
                        categoria_proyectos.COMENTARIO = string.Empty;
                    }
                    // FIN VALIDACIONES
                    // Actualiza el empleado en la base de datos utilizando el procedimiento almacenado
                    context.Database.ExecuteSqlCommand("sp_EditarCategoria_Proyectos @codigo_categoria, @nombre_categoria, @comentario",
                        new SqlParameter("@codigo_categoria", categoria_proyectos.CODIGO_CATEGORIA),
                        new SqlParameter("@nombre_categoria", categoria_proyectos.NOMBRE_CATEGORIA),
                        new SqlParameter("@comentario", categoria_proyectos.COMENTARIO)
                    );

                    return Json(new { success = true });
                }
            }

            return Json(new { success = false, message = "Error al actualizar la categoría del proyecto." });
        }


        //FIN DE EDITAR EN GRID



        //INICIO ELIMINAR EN GRID
        public ActionResult EliminarCategoria_Proyectos(string codigoCategoria)
        {
            // Llama al procedimiento almacenado para eliminar la categoria
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                context.Database.ExecuteSqlCommand("exec sp_EliminaCategoria_Proyectos @codigo_categoria", new SqlParameter("@codigo_categoria", codigoCategoria));
            }


            return RedirectToAction("VistaCategoria_Proyectos");
        }


        //FIN ELIMINAR EN GRID







    }
}