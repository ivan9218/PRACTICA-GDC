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

namespace DISETOP.Controllers
{
    public class ActivosController : Controller
    {
        private DISETOPEntities db = new DISETOPEntities();
        // GET: ActivosDisetop
        public ActionResult Index()
        {
            return View();
        }

        //INICIO DE LISTAR EN GRID
        public ActionResult VistaActivos()
        {
            List<ACTIVO> activos = new List<ACTIVO>();

            using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_RetornaActivos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ACTIVO activo = new ACTIVO
                            {
                                CODIGO_ACTIVO = reader["CODIGO_ACTIVO"].ToString(),
                                NOMBRE_DE_ACTIVO = reader["NOMBRE_DE_ACTIVO"].ToString(),
                                NUMERO_DE_SERIE = reader["NUMERO_DE_SERIE"].ToString(),
                                PROVEEDOR = reader["PROVEEDOR"].ToString(),
                                COMENTARIO = reader["COMENTARIO"].ToString()
                            };

                            // Añadir manejo de excepciones para CEDULA y TELEFONO
                            try
                            {
                                //VALIDA SI LA FECHA NO ES NULA ENTONCES LA CONVIERTE Y LA CARGA
                                // Y SI ES NULA ENTONCES SE QUEDA ASI NULA Y SE ESCRIBE EL MENSAJE QUE ESTA EN LA VALIDACION DLE HTMMT OSEA EN LA VISTA. 
                                if (reader["FECHA_DE_COMPRA"] != DBNull.Value)
                                {
                                    activo.FECHA_DE_COMPRA = Convert.ToDateTime(reader["FECHA_DE_COMPRA"]);
                                }
                               
                                activo.VALOR_DE_COMPRA = Convert.ToDecimal(reader["VALOR_DE_COMPRA"]);
                                activo.VALOR_ACTUAL = Convert.ToDecimal(reader["VALOR_ACTUAL"]);

                            }
                            catch (Exception)
                            {
                                return Json(new { success = false, message = "Error al cargar datos de la tabla" });
                            }

                            activos.Add(activo);
                        }
                    }
                }
            }

            return View(activos);
        }


        //FIN DE LISTAR EN GRID

        //INICIO DE INSERTAR EN GRID

        [HttpGet]
        public ActionResult InsertarActivo()
        {
            return View(); // Esta vista mostrará el formulario de inserción
        }

        [HttpPost]
        public ActionResult InsertarActivo(ACTIVO activo)
        {
            // INICIO VALIDACIONES
            try
            {
                if (string.IsNullOrWhiteSpace(activo.CODIGO_ACTIVO))
                {
                    return Json(new { success = false, message = "El campo 'Codigo de Activo' es obligatorio." });
                }
                if (string.IsNullOrWhiteSpace(activo.NOMBRE_DE_ACTIVO))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Activo' es obligatorio." });
                }
                if (string.IsNullOrWhiteSpace(activo.NUMERO_DE_SERIE))
                {
                    return Json(new { success = false, message = "El campo 'Número de Serie' es obligatorio." });
                }
                if (activo.FECHA_DE_COMPRA != null)
                {
                    activo.FECHA_DE_COMPRA = activo.FECHA_DE_COMPRA.Value.Date; // Elimina la parte de la hora y los minutos
                }
                //if (activo.FECHA_DE_COMPRA == null)
                //{
                //    activo.FECHA_DE_COMPRA = new DateTime(1753, 1, 1);

                //}

                // Asigna cadena vacía a proveedor si está nulo
                if (string.IsNullOrWhiteSpace(activo.PROVEEDOR))
                {
                    activo.PROVEEDOR = string.Empty;
                }

                // Verifica si el MONTO no es nulo y está fuera del rango permitido
                if (activo.VALOR_DE_COMPRA != null)
                {
                    if (activo.VALOR_DE_COMPRA < 0 || activo.VALOR_DE_COMPRA > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Valor de compra' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    activo.VALOR_DE_COMPRA = 0;
                }
                // Verifica si el MONTO no es nulo y está fuera del rango permitido
                if (activo.VALOR_ACTUAL != null)
                {
                    if (activo.VALOR_ACTUAL < 0 || activo.VALOR_ACTUAL > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Valor actual' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    activo.VALOR_ACTUAL = 0;
                }
                // Asigna cadena vacía a proveedor si está nulo
                if (string.IsNullOrWhiteSpace(activo.COMENTARIO))
                {
                    activo.COMENTARIO = string.Empty;
                }

                // Inserta el nuevo empleado en la base de datos utilizando el procedimiento almacenado
                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    var activoExistente = context.ACTIVOS.FirstOrDefault(e => e.CODIGO_ACTIVO == activo.CODIGO_ACTIVO);

                    if (activoExistente != null)
                    {
                        return Json(new { success = false, message = "El Activo con el código " + activo.CODIGO_ACTIVO + " ya existe." });
                    }

                    var activoExistentePorSerie = context.ACTIVOS.FirstOrDefault(e => e.NUMERO_DE_SERIE == activo.NUMERO_DE_SERIE);

                    if (activoExistentePorSerie != null)
                    {
                        return Json(new { success = false, message = "El activo con el número de serie " + activo.NUMERO_DE_SERIE + " ya existe." });
                    }
                    // FIN VALIDACIONES

                    context.Database.ExecuteSqlCommand("sp_InsertaActivos @codigo_activo, @nombre_de_activo, @numero_de_serie, @FechaDeCompra, @Proveedor, @Valor_de_compra, @Valor_actual, @comentario",
                        new SqlParameter("@codigo_activo", activo.CODIGO_ACTIVO),
                        new SqlParameter("@nombre_de_activo", activo.NOMBRE_DE_ACTIVO),
                        new SqlParameter("@numero_de_serie", activo.NUMERO_DE_SERIE),
                        new SqlParameter("@FechaDeCompra", activo.FECHA_DE_COMPRA.HasValue ? (object)activo.FECHA_DE_COMPRA.Value : DBNull.Value),
                        //new SqlParameter("@FechaDeCompra", activo.FECHA_DE_COMPRA),
                        new SqlParameter("@Proveedor", activo.PROVEEDOR),
                        new SqlParameter("@Valor_de_compra", activo.VALOR_DE_COMPRA),
                        new SqlParameter("@Valor_actual", activo.VALOR_ACTUAL),
                        new SqlParameter("@comentario", activo.COMENTARIO)
                    );
                }

                return Json(new { success = true });
               
            }
            catch (Exception ex)
            {
                // Maneja la excepción y devuelve un mensaje de error
                return Json(new { success = false, message = "Error al actualizar el activo. Detalles: " + ex.Message });
            }
        }


        //FIN DE INSERTAR EN GRID

        //INICIO DE EDITAR EN GRID

        [HttpGet]
        public ActionResult EditarActivo(string codigo_activo)
        {
            // Recuperar el activo de la base de datos usando el código_activo proporcionado
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                ACTIVO activo = context.ACTIVOS.FirstOrDefault(e => e.CODIGO_ACTIVO == codigo_activo);

                if (activo != null)
                {
                    return View(activo); // Mostrar la vista de edición con los datos del activo
                }
                else
                {
                    return HttpNotFound(); // Devolver un error 404 si no se encuentra el activo
                }
            }
        }

        [HttpPost]
        public ActionResult EditarActivo(ACTIVO activo)
        {
            //if (ModelState.IsValid)
            //{try

            try
            {
                // Inserta el nuevo activo en la base de datos utilizando el procedimiento almacenado
                using (var context = new DISETOP.Models.DISETOPEntities())
                {

                // INICIO VALIDACIONES


                // Verifica si el MONTO no es nulo y está fuera del rango permitido
                if (activo.VALOR_DE_COMPRA != null)
                {
                    if (activo.VALOR_DE_COMPRA < 0 || activo.VALOR_DE_COMPRA > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Valor de compra' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    activo.VALOR_DE_COMPRA = 0;
                }
                // Verifica si el MONTO no es nulo y está fuera del rango permitido
                if (activo.VALOR_ACTUAL != null)
                {
                    if (activo.VALOR_ACTUAL < 0 || activo.VALOR_ACTUAL > 9999999999999999.99m)
                    {
                        return Json(new { success = false, message = "El campo 'Valor actual' está fuera del rango permitido." });
                    }
                }
                else
                {
                    // Asigna 0 a MONTO si es nulo
                    activo.VALOR_ACTUAL = 0;
                }


                if (string.IsNullOrWhiteSpace(activo.CODIGO_ACTIVO))
                    {
                        return Json(new { success = false, message = "El campo 'Codigo de Activo' es obligatorio." });
                    }
                    if (string.IsNullOrWhiteSpace(activo.NOMBRE_DE_ACTIVO))
                    {
                        return Json(new { success = false, message = "El campo 'Nombre de Activo' es obligatorio." });
                    }
                    if (string.IsNullOrWhiteSpace(activo.NUMERO_DE_SERIE))
                    {
                        return Json(new { success = false, message = "El campo 'Número de Serie' es obligatorio." });
                    }
                    //if (activo.FECHA_DE_COMPRA != null)
                    //{
                    //    activo.FECHA_DE_COMPRA = activo.FECHA_DE_COMPRA.Value.Date; // Elimina la parte de la hora y los minutos
                    //}

                    // Asigna cadena vacía a proveedor si está nulo
                    if (string.IsNullOrWhiteSpace(activo.PROVEEDOR))
                    {
                        activo.PROVEEDOR = string.Empty;
                    }

                    if (string.IsNullOrWhiteSpace(activo.COMENTARIO))
                    {
                        activo.COMENTARIO = string.Empty;
                    }

                    // Inserta el nuevo activo en la base de datos utilizando el procedimiento almacenado
                    // Verifica si existe otro activo con el mismo número de serie (ignorando el actual que se está editando)
                    var activoExistentePorSerie = context.ACTIVOS.FirstOrDefault(e => e.NUMERO_DE_SERIE == activo.NUMERO_DE_SERIE && e.CODIGO_ACTIVO != activo.CODIGO_ACTIVO);

                    if (activoExistentePorSerie != null)
                    {
                        return Json(new { success = false, message = "El activo con el número de serie " + activo.NUMERO_DE_SERIE + " ya existe." });
                    }
                    // FIN VALIDACIONES

                    context.Database.ExecuteSqlCommand("sp_EditarActivos @CodigoActivo, @Nombre_de_activo, @Numero_de_serie, @Fecha_de_compra, @Proveedor, @Valor_de_compra, @Valor_actual, @Comentario",
                    new SqlParameter("@CodigoActivo", activo.CODIGO_ACTIVO),
                    new SqlParameter("@Nombre_de_activo", activo.NOMBRE_DE_ACTIVO),
                    new SqlParameter("@Numero_de_serie", activo.NUMERO_DE_SERIE),
                    new SqlParameter("@Fecha_de_compra", activo.FECHA_DE_COMPRA.HasValue ? (object)activo.FECHA_DE_COMPRA.Value : DBNull.Value),
                    new SqlParameter("@Proveedor", activo.PROVEEDOR),
                    new SqlParameter("@Valor_de_compra", activo.VALOR_DE_COMPRA),
                    new SqlParameter("@Valor_actual", activo.VALOR_ACTUAL),
                    new SqlParameter("@Comentario", activo.COMENTARIO)
                );


                    return Json(new { success = true });

                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción y devuelve un mensaje de error
                return Json(new { success = false, message = "Error al actualizar el activo. Detalles: " + ex.Message });
            }

        }


        //FIN DE EDITAR EN GRID

        //INICIO ELIMINAR EN GRID
        public ActionResult EliminarActivo(string codigoActivo)
        {
            // Llama al procedimiento almacenado para eliminar el activo
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                context.Database.ExecuteSqlCommand("exec sp_EliminaActivos @codigo_activo", new SqlParameter("@codigo_activo", codigoActivo));
            }


            return RedirectToAction("VistaActivos");
        }


        //FIN ELIMINAR EN GRID





    }
}