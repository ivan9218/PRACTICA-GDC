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
    public class Costo_ProyectosController : Controller
    {
        private DISETOPEntities db = new DISETOPEntities();
        // GET: Costo_Proyectos
        //***********INICIO DE LISTAR TOTAL CON FORANEA EN GRID Y MODALS*************
        public ActionResult Index()
        {
            return View();
        }
        public List<COSTO_PROYECTOS> getCostoProyectos()
        {
            List<COSTO_PROYECTOS> costo_proyectos = new List<COSTO_PROYECTOS>();
            using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_RetornaCostoProyectos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            COSTO_PROYECTOS costo_proyecto = new COSTO_PROYECTOS
                            {
                                CODIGO_COSTO_PROYECTO = reader["CODIGO_COSTO_PROYECTO"].ToString(),
                                FK_CODIGO_PROYECTO = reader["FK_CODIGO_PROYECTO"].ToString(),
                                FK_CODIGO_CATEGORIA = reader["FK_CODIGO_CATEGORIA"].ToString(),
                                COMENTARIO = reader["COMENTARIO"].ToString()
                            };

                            // Añadir manejo de excepciones para MONTO y FECHA DE PAGO
                            try
                            {
                                costo_proyecto.DIA_TRABAJADOR = Convert.ToDecimal(reader["DIA_TRABAJADOR"]);
                                costo_proyecto.COMBUSTIBLE = Convert.ToDecimal(reader["COMBUSTIBLE"]);
                                costo_proyecto.VIATICOS = Convert.ToDecimal(reader["VIATICOS"]);
                                costo_proyecto.HOSPEDAJE = Convert.ToDecimal(reader["HOSPEDAJE"]);
                                costo_proyecto.TIMBRES_CERTIFICACIONES = Convert.ToDecimal(reader["TIMBRES_CERTIFICACIONES"]);
                                costo_proyecto.MATERIALES = Convert.ToDecimal(reader["MATERIALES"]);
                                costo_proyecto.TOTAL = Convert.ToDecimal(reader["TOTAL"]);
                               
                            }
                            catch (Exception)
                            {
                                // Manejar excepciones
                            }

                            // Obtener el nombre del empleado a partir del código de empleado
                            var codigoCategoria = costo_proyecto.FK_CODIGO_CATEGORIA; // El código de empleado es una cadena (varchar)
                            var categoria = db.CATEGORIA_PROYECTOS.FirstOrDefault(e => e.CODIGO_CATEGORIA == codigoCategoria);

                            if (categoria != null)
                            {
                                costo_proyecto.NOMBRE_CATEGORIA = categoria.NOMBRE_CATEGORIA;
                            }

                            // Obtener el nombre del empleado a partir del código de empleado
                            var codigoProyecto = costo_proyecto.FK_CODIGO_PROYECTO; // El código de empleado es una cadena (varchar)
                            var proyecto = db.PROYECTOS.FirstOrDefault(e => e.CODIGO_PROYECTO == codigoProyecto);

                            if (categoria != null)
                            {
                                costo_proyecto.NOMBRE_PROYECTO = proyecto.NOMBRE_PROYECTO;
                            }

                            costo_proyectos.Add(costo_proyecto);
                        }
                    }
                }
            }

            return costo_proyectos;
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
        // ESTE ES EL QUE CARGA LOS DATOS DE EMPLEADO OSEA DE LA FORANEA EN EL MODAL DE INSERTAR
        //ESTA REPETIDO HAY QUE LLAMARLO DE DONDE ESTA 
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

                                proyecto.FECHA_FIN = Convert.ToDateTime(reader["FECHA_FIN"]);
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



        //INICIO LISTAR EN EL MODAL
        public ActionResult VistaCostoProyectos()
        {
            List<COSTO_PROYECTOS> costo_proyectos = getCostoProyectos();
            List<PROYECTO> proyectos = getProyectos();
            List<CATEGORIA_PROYECTOS> categorias = getCategorias();
            var model = new Proyectos_Categorias_CostoProyectos_Model
            {
                Costo_Proyectos = costo_proyectos,
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

        [HttpGet] // Puedes usar [HttpGet] si corresponde
        public ActionResult ObtenerNombreProyecto(string codigoProyecto)
        {
            var proyecto = db.PROYECTOS.FirstOrDefault(e => e.CODIGO_PROYECTO == codigoProyecto);

            if (proyecto != null)
            {
                return Json(proyecto.NOMBRE_PROYECTO, JsonRequestBehavior.AllowGet);
            }

            // Manejar el caso donde no se encuentra el empleado
            return Json("No se encontró el Proyecto", JsonRequestBehavior.AllowGet);
        }




        // FIN LISTAR JALANDO NOMBRE EMPLEADO A PARTIR DE LLAVE FORANEA 

        //***********FIN DE LISTAR TOTAL CON FORANEA EN GRID Y MODALS*************



        //INICIO DE INSERTAR EN GRID
        [HttpGet]
        public ActionResult InsertarCostoProyecto()
        {
            // Aquí deberías cargar los datos necesarios para el formulario de inserción de costo proyecto, si es necesario.
            return View();
        }

        [HttpPost]
        public ActionResult InsertarCostoProyecto(COSTO_PROYECTOS costo_proyecto)
        {

            if (ModelState.IsValid)
            {

                //INICIO VALIDACIONES

                //*******INICIO************
                if (costo_proyecto.DIA_TRABAJADOR != null)
                {
                    if (costo_proyecto.DIA_TRABAJADOR < 0 || costo_proyecto.DIA_TRABAJADOR > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Día Trabajador' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.DIA_TRABAJADOR = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.COMBUSTIBLE != null)
                {
                    if (costo_proyecto.COMBUSTIBLE < 0 || costo_proyecto.COMBUSTIBLE > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Combustible' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.COMBUSTIBLE = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.COMBUSTIBLE != null)
                {
                    if (costo_proyecto.COMBUSTIBLE < 0 || costo_proyecto.COMBUSTIBLE > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Combustible' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.COMBUSTIBLE = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.VIATICOS != null)
                {
                    if (costo_proyecto.VIATICOS < 0 || costo_proyecto.VIATICOS > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Viáticos' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.VIATICOS = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.HOSPEDAJE != null)
                {
                    if (costo_proyecto.HOSPEDAJE < 0 || costo_proyecto.HOSPEDAJE > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Hospedaje' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.HOSPEDAJE = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.TIMBRES_CERTIFICACIONES != null)
                {
                    if (costo_proyecto.TIMBRES_CERTIFICACIONES < 0 || costo_proyecto.TIMBRES_CERTIFICACIONES > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Timbres Certificaciones' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.TIMBRES_CERTIFICACIONES = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.MATERIALES != null)
                {
                    if (costo_proyecto.MATERIALES < 0 || costo_proyecto.MATERIALES > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Materiales' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.MATERIALES = 0;
                }
                //*******FIN************

                // Asigna cadena vacía a COMENTARIO si está nulo
                if (string.IsNullOrWhiteSpace(costo_proyecto.COMENTARIO))
                {
                    costo_proyecto.COMENTARIO = string.Empty;
                }
              
                if (string.IsNullOrWhiteSpace(costo_proyecto.CODIGO_COSTO_PROYECTO))
                {
                    return Json(new { success = false, message = "El campo 'Código Costo de Proyecto' es obligatorio." });
                }

                if (string.IsNullOrWhiteSpace(costo_proyecto.FK_CODIGO_PROYECTO))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Proyecto' es obligatorio." });
                }

                if (string.IsNullOrWhiteSpace(costo_proyecto.FK_CODIGO_CATEGORIA))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Categoría' es obligatorio." });
                }

                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    var CostoproyectoExistente = context.COSTO_PROYECTOS.FirstOrDefault(p => p.CODIGO_COSTO_PROYECTO == costo_proyecto.CODIGO_COSTO_PROYECTO);

                    if (CostoproyectoExistente != null)
                    {
                        return Json(new { success = false, message = "El costo proyecto con el código " + costo_proyecto.CODIGO_COSTO_PROYECTO + " ya existe." });
                    }

                    //FIN VALIDACIONES
                    context.Database.ExecuteSqlCommand("sp_InsertarCostoProyecto @CodigoCostoProyecto, @FK_CodigoProyecto, @FK_CodigoCategoria, @DiaTrabajador, @Combustible, @Viaticos, @Hospedaje,@TimbresCertificaciones,@Materiales, @Comentario",
                        new SqlParameter("@CodigoCostoProyecto", costo_proyecto.CODIGO_COSTO_PROYECTO),
                        new SqlParameter("@FK_CodigoProyecto", costo_proyecto.FK_CODIGO_PROYECTO),
                        new SqlParameter("@FK_CodigoCategoria", costo_proyecto.FK_CODIGO_CATEGORIA),
                        new SqlParameter("@DiaTrabajador", costo_proyecto.DIA_TRABAJADOR),
                        new SqlParameter("@Combustible", costo_proyecto.COMBUSTIBLE),
                        new SqlParameter("@Viaticos", costo_proyecto.VIATICOS),
                        new SqlParameter("@Hospedaje", costo_proyecto.HOSPEDAJE),
                        new SqlParameter("@TimbresCertificaciones", costo_proyecto.TIMBRES_CERTIFICACIONES),
                        new SqlParameter("@Materiales", costo_proyecto.MATERIALES),
                        new SqlParameter("@Comentario", costo_proyecto.COMENTARIO)
                    );
                }

                return Json(new { success = true });
            }


            return Json(new { success = false, message = "Error al agregar el costo del proyecto." });
        }
        //FIN DE INSERTAR EN GRID



        //INICIO EDITAR COSTO PROYECTOS
        [HttpGet]
        public ActionResult EditarCostoProyecto(string codigo_costo_proyecto)
        {
            // Recuperar el pago de la base de datos usando el codigo_pago proporcionado
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                COSTO_PROYECTOS costo_proyecto = context.COSTO_PROYECTOS.FirstOrDefault(e => e.CODIGO_COSTO_PROYECTO == codigo_costo_proyecto);

                if (costo_proyecto != null)
                {
                    return View(costo_proyecto); // Mostrar la vista de edición con los datos del pago
                }
                else
                {
                    return HttpNotFound(); // Devolver un error 404 si no se encuentra el empleado
                }
            }
        }

        [HttpPost]
        public ActionResult EditarCostoProyecto(COSTO_PROYECTOS costo_proyecto)
        {

            if (ModelState.IsValid)
            {

                //INICIO VALIDACIONES

                //*******INICIO************
                if (costo_proyecto.DIA_TRABAJADOR != null)
                {
                    if (costo_proyecto.DIA_TRABAJADOR < 0 || costo_proyecto.DIA_TRABAJADOR > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Día Trabajador' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.DIA_TRABAJADOR = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.COMBUSTIBLE != null)
                {
                    if (costo_proyecto.COMBUSTIBLE < 0 || costo_proyecto.COMBUSTIBLE > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Combustible' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.COMBUSTIBLE = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.COMBUSTIBLE != null)
                {
                    if (costo_proyecto.COMBUSTIBLE < 0 || costo_proyecto.COMBUSTIBLE > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Combustible' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.COMBUSTIBLE = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.VIATICOS != null)
                {
                    if (costo_proyecto.VIATICOS < 0 || costo_proyecto.VIATICOS > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Viáticos' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.VIATICOS = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.HOSPEDAJE != null)
                {
                    if (costo_proyecto.HOSPEDAJE < 0 || costo_proyecto.HOSPEDAJE > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Hospedaje' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.HOSPEDAJE = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.TIMBRES_CERTIFICACIONES != null)
                {
                    if (costo_proyecto.TIMBRES_CERTIFICACIONES < 0 || costo_proyecto.TIMBRES_CERTIFICACIONES > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Timbres Certificaciones' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.TIMBRES_CERTIFICACIONES = 0;
                }
                //*******FIN************

                //*******INICIO************
                if (costo_proyecto.MATERIALES != null)
                {
                    if (costo_proyecto.MATERIALES < 0 || costo_proyecto.MATERIALES > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Materiales' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    costo_proyecto.MATERIALES = 0;
                }
                //*******FIN************

                // Asigna cadena vacía a COMENTARIO si está nulo
                if (string.IsNullOrWhiteSpace(costo_proyecto.COMENTARIO))
                {
                    costo_proyecto.COMENTARIO = string.Empty;
                }

                //if (string.IsNullOrWhiteSpace(costo_proyecto.CODIGO_COSTO_PROYECTO))
                //{
                //    return Json(new { success = false, message = "El campo 'Código Costo de Proyecto' es obligatorio." });
                //}



                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    //var CostoproyectoExistente = context.COSTO_PROYECTOS.FirstOrDefault(p => p.CODIGO_COSTO_PROYECTO == costo_proyecto.CODIGO_COSTO_PROYECTO);

                    //if (CostoproyectoExistente != null)
                    //{
                    //    return Json(new { success = false, message = "El costo proyecto con el código " + costo_proyecto.CODIGO_COSTO_PROYECTO + " ya existe." });
                    //}

                    //FIN VALIDACIONES
                    context.Database.ExecuteSqlCommand("sp_EditarCostoProyecto @CodigoCostoProyecto, @FK_CodigoProyecto, @FK_CodigoCategoria, @DiaTrabajador, @Combustible, @Viaticos, @Hospedaje,@TimbresCertificaciones,@Materiales, @Comentario",
                        new SqlParameter("@CodigoCostoProyecto", costo_proyecto.CODIGO_COSTO_PROYECTO),
                        new SqlParameter("@FK_CodigoProyecto", costo_proyecto.FK_CODIGO_PROYECTO),
                        new SqlParameter("@FK_CodigoCategoria", costo_proyecto.FK_CODIGO_CATEGORIA),
                        new SqlParameter("@DiaTrabajador", costo_proyecto.DIA_TRABAJADOR),
                        new SqlParameter("@Combustible", costo_proyecto.COMBUSTIBLE),
                        new SqlParameter("@Viaticos", costo_proyecto.VIATICOS),
                        new SqlParameter("@Hospedaje", costo_proyecto.HOSPEDAJE),
                        new SqlParameter("@TimbresCertificaciones", costo_proyecto.TIMBRES_CERTIFICACIONES),
                        new SqlParameter("@Materiales", costo_proyecto.MATERIALES),
                        new SqlParameter("@Comentario", costo_proyecto.COMENTARIO)
                    );
                }

                return Json(new { success = true });
            }


            return Json(new { success = false, message = "Error al Actualizar el costo del proyecto." });
        }
        //FIN DE EDITAR EN GRID


        //INICIO ELIMINAR EN GRID
        public ActionResult EliminarCostoProyecto(string CodigoCostoProyecto)
        {
            // Llama al procedimiento almacenado para eliminar el pago
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                context.Database.ExecuteSqlCommand("exec sp_EliminaCostoProyectos @CodigoCostoProyecto", new SqlParameter("@CodigoCostoProyecto", CodigoCostoProyecto));
            }


            return RedirectToAction("VistaCostoProyectos");
        }


        //FIN ELIMINAR EN GRID







    }
}