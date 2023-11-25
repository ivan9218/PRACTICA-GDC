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
using Antlr.Runtime.Misc;

namespace DISETOP.Controllers
{
    public class PagosController : Controller
    {

        private DISETOPEntities db = new DISETOPEntities();

        // GET: Pagos
        public ActionResult Index()
        {
            return View();
        }

        public List<PAGO> getPagos() {
            List<PAGO> pagos = new List<PAGO>();
            using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_RetornaPagos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PAGO pago = new PAGO
                            {
                                CODIGO_PAGO = reader["CODIGO_PAGO"].ToString(),
                                FK_CODIGO_EMPLEADO = reader["FK_CODIGO_EMPLEADO"].ToString(),
                                CUENTA_BANCARIA = reader["CUENTA_BANCARIA"].ToString(),
                                DIAS_A_PAGAR = reader["DIAS_A_PAGAR"].ToString(),
                                COMENTARIO = reader["COMENTARIO"].ToString()
                            };

                            // Añadir manejo de excepciones para MONTO y FECHA DE PAGO
                            try
                            {
                                pago.MONTO = Convert.ToDecimal(reader["MONTO"]);
                                pago.FECHA_DE_PAGO = Convert.ToDateTime(reader["FECHA_DE_PAGO"]);
                            }
                            catch (Exception)
                            {
                                // Manejar excepciones
                            }

                            // Obtener el nombre del empleado a partir del código de empleado
                            var codigoEmpleado = pago.FK_CODIGO_EMPLEADO; // El código de empleado es una cadena (varchar)
                            var empleado = db.EMPLEADOS.FirstOrDefault(e => e.CODIGO_EMPLEADO == codigoEmpleado);

                            if (empleado != null)
                            {
                                pago.NOMBRE_EMPLEADO = empleado.NOMBRE_EMPLEADO;
                            }


                            pagos.Add(pago);
                        }
                    }
                }
            }

            return pagos;
        }
        // ESTE ES EL QUE CARGA LOS DATOS DE EMPLEADO OSEA DE LA FORANEA EN EL MODAL DE INSERTAR
        //ESTA REPETIDO HAY QUE LLAMARLO DE DONDE ESTA 
        public List<EMPLEADO> getEmpleados()
        {
            List<EMPLEADO> empleados = new List<EMPLEADO>();

            using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_RetornaEmpleados", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EMPLEADO empleado = new EMPLEADO
                            {
                                CODIGO_EMPLEADO = reader["CODIGO_EMPLEADO"].ToString(),
                                NOMBRE_EMPLEADO = reader["NOMBRE_EMPLEADO"].ToString(),
                                CORREO = reader["CORREO"].ToString(),
                                DIRECCION = reader["DIRECCION"].ToString(),
                                COMENTARIO = reader["COMENTARIO"].ToString()
                            };

                            // Añadir manejo de excepciones para CEDULA y TELEFONO
                            try
                            {
                                empleado.CEDULA = Convert.ToInt32(reader["CEDULA"]);
                                empleado.TELEFONO = Convert.ToInt32(reader["TELEFONO"]);
                            }
                            catch (Exception)
                            {

                            }

                            empleados.Add(empleado);
                        }
                    }
                }
            }

            return empleados;

        }

        
        //INICIO LISTAR EN EL MODAL
        public ActionResult VistaPagos()
        {
            List<PAGO> pagos = getPagos();
            List<EMPLEADO> empleados = getEmpleados();
            var model = new PagosEmpleadosModel
            {
                Pagos = pagos,
                Empleados = empleados
            };


            return View(model);
        }
        // FIN DE LISTAR EN EL MODAL 
        // Método para obtener el nombre del empleado ESTE ES PARA LISTAR EN EL GRID LO QUE YO OCUPE BASADO EN EL ID DE LA LLAVE FORANEA
        [HttpGet] // Puedes usar [HttpGet] si corresponde
        public ActionResult ObtenerNombreEmpleado(string codigoEmpleado)
        {
            var empleado = db.EMPLEADOS.FirstOrDefault(e => e.CODIGO_EMPLEADO == codigoEmpleado);

            if (empleado != null)
            {
                return Json(empleado.NOMBRE_EMPLEADO, JsonRequestBehavior.AllowGet);
            }

            // Manejar el caso donde no se encuentra el empleado
            return Json("No se encontró el empleado", JsonRequestBehavior.AllowGet);
        }

        // FIN LISTAR JALANDO NOMBRE EMPLEADO A PARTIR DE LLAVE FORANEA 


        //INICIO INSERTAR PAGOS

        //INICIO DE INSERTAR EN GRID
        [HttpGet]
        public ActionResult InsertarPago()
        {
            // Aquí deberías cargar los datos necesarios para el formulario de inserción de pagos, si es necesario.
            return View();
        }

        [HttpPost]
        public ActionResult InsertarPago(PAGO pago)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DISETOP.Models.DISETOPEntities())
                {

                    //INICIO VALIDACIONES

                    var pagoExistente = context.PAGOS.FirstOrDefault(p => p.CODIGO_PAGO == pago.CODIGO_PAGO);

                    if (pagoExistente != null)
                    {
                        return Json(new { success = false, message = "El pago con el código " + pago.CODIGO_PAGO + " ya existe." });
                    }

                    // Verifica si el MONTO no es nulo y está fuera del rango permitido
                    if (pago.MONTO != null)
                    {
                        if (pago.MONTO < 0 || pago.MONTO > 9999999999999999.99m)
                        {
                            return Json(new { success = false, message = "El campo 'Monto' está fuera del rango permitido." });
                        }
                    }
                    else
                    {
                        // Asigna 0 a MONTO si es nulo
                        pago.MONTO = 0;
                    }

                    // Asigna cadena vacía a COMENTARIO si está nulo
                    if (string.IsNullOrWhiteSpace(pago.COMENTARIO))
                 {
                   pago.COMENTARIO = string.Empty;
                 }
                // Asigna cadena vacía a CUENTA BANCARIA si está nulo
                if (string.IsNullOrWhiteSpace(pago.CUENTA_BANCARIA))
                {
                    pago.CUENTA_BANCARIA = string.Empty;
                }
                // Asigna cadena vacía a DIAS A PAGAR si está nulo
                if (string.IsNullOrWhiteSpace(pago.DIAS_A_PAGAR))
                {
                    pago.DIAS_A_PAGAR = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(pago.CODIGO_PAGO))
                {
                    return Json(new { success = false, message = "El campo 'Código de Pago' es obligatorio." });
                }
                if (string.IsNullOrWhiteSpace(pago.FK_CODIGO_EMPLEADO))
                {
                    return Json(new { success = false, message = "El campo 'Codigo de Empleado' es obligatorio." });
                }
                if (pago.FECHA_DE_PAGO != null)
                {
                    pago.FECHA_DE_PAGO = pago.FECHA_DE_PAGO.Value.Date; // Elimina la parte de la hora y los minutos
                }

                    //var formattedFechaDePago = pago.FECHA_DE_PAGO.ToString("yyyy-MM-dd"); // Formatea la fecha a 'yyyy-MM-dd'

                    //FIN VALIDACIONES
                    context.Database.ExecuteSqlCommand("sp_InsertarPagoEmpleado @Codigo_pago, @FK_CodigoEmpleado, @CuentaBancaria, @DiasAPagar, @Monto, @FechaDePago, @Comentario",
                        new SqlParameter("@Codigo_pago", pago.CODIGO_PAGO),
                        new SqlParameter("@FK_CodigoEmpleado", pago.FK_CODIGO_EMPLEADO),
                        new SqlParameter("@CuentaBancaria", pago.CUENTA_BANCARIA),
                        new SqlParameter("@DiasAPagar", pago.DIAS_A_PAGAR),
                        new SqlParameter("@Monto", pago.MONTO),
                        new SqlParameter("@FechaDePago", pago.FECHA_DE_PAGO.HasValue ? (object)pago.FECHA_DE_PAGO.Value : DBNull.Value),
                        new SqlParameter("@Comentario", pago.COMENTARIO)
                    );
                }

                return Json(new { success = true });
            }

            return View(pago);
        }
        //FIN DE INSERTAR EN GRID

        //FIN INSERTAR PAGOS

        //INICIO EDITAR PAGOS
        [HttpGet]
        public ActionResult EditarPago(string codigo_pago)
        {
            // Recuperar el pago de la base de datos usando el codigo_pago proporcionado
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                PAGO pago = context.PAGOS.FirstOrDefault(e => e.CODIGO_PAGO == codigo_pago);

                if (pago != null)
                {
                    return View(pago); // Mostrar la vista de edición con los datos del pago
                }
                else
                {
                    return HttpNotFound(); // Devolver un error 404 si no se encuentra el empleado
                }
            }
        }

        [HttpPost]
        public ActionResult EditarPago(PAGO pago)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    //INICIO VALIDACIONES


                       // Verifica si el MONTO no es nulo y está fuera del rango permitido
                    if (pago.MONTO != null)
                    {
                        if (pago.MONTO < 0 || pago.MONTO > 9999999999999999.99m)
                        {
                            return Json(new { success = false, message = "El campo 'Monto' está fuera del rango permitido." });
                        }
                    }
                    else
                    {
                        // Asigna 0 a MONTO si es nulo
                        pago.MONTO = 0;
                    }

                    // Asigna cadena vacía a COMENTARIO si está nulo
                    if (string.IsNullOrWhiteSpace(pago.COMENTARIO))
                    {
                        pago.COMENTARIO = string.Empty;
                    }
                    // Asigna cadena vacía a CUENTA BANCARIA si está nulo
                    if (string.IsNullOrWhiteSpace(pago.CUENTA_BANCARIA))
                    {
                        pago.CUENTA_BANCARIA = string.Empty;
                    }
                    // Asigna cadena vacía a DIAS A PAGAR si está nulo
                    if (string.IsNullOrWhiteSpace(pago.DIAS_A_PAGAR))
                    {
                        pago.DIAS_A_PAGAR = string.Empty;
                    }
                    if (string.IsNullOrWhiteSpace(pago.CODIGO_PAGO))
                    {
                        return Json(new { success = false, message = "El campo 'Código de Pago' es obligatorio." });
                    }
                    if (string.IsNullOrWhiteSpace(pago.FK_CODIGO_EMPLEADO))
                    {
                        return Json(new { success = false, message = "El campo 'Nombre de Empleado' es obligatorio." });
                    }
                    if (pago.FECHA_DE_PAGO != null)
                    {
                        pago.FECHA_DE_PAGO = pago.FECHA_DE_PAGO.Value.Date; // Elimina la parte de la hora y los minutos
                    }
                 
                    
                    context.Database.ExecuteSqlCommand("sp_EditarPagos @CodigoPago, @FK_CodigoEmpleado, @CuentaBancaria, @DiasAPagar, @Monto, @FechaDePago, @Comentario",
                            new SqlParameter("@CodigoPago", pago.CODIGO_PAGO),
                            new SqlParameter("@FK_CodigoEmpleado", pago.FK_CODIGO_EMPLEADO),
                            new SqlParameter("@CuentaBancaria", pago.CUENTA_BANCARIA), 
                            new SqlParameter("@DiasAPagar", pago.DIAS_A_PAGAR),
                            new SqlParameter("@Monto", pago.MONTO),
                            new SqlParameter("@FechaDePago", pago.FECHA_DE_PAGO.HasValue ? (object)pago.FECHA_DE_PAGO.Value : DBNull.Value),
                            new SqlParameter("@Comentario", pago.COMENTARIO)
                        );
                    

                    return Json(new { success = true });
                }
                  
            }

            return Json(new { success = false, message = "Error al actualizar el pago." });
        }

        //FIN EDITAR PAGOS


        //INICIO ELIMINAR EN GRID
        public ActionResult EliminarPago(string CodigoPago)
        {
            // Llama al procedimiento almacenado para eliminar el pago
            using (var context = new DISETOP.Models.DISETOPEntities()) 
            {
                context.Database.ExecuteSqlCommand("exec sp_EliminaPagos @CodigoPago", new SqlParameter("@CodigoPago", CodigoPago));
            }

       
            return RedirectToAction("VistaPagos");
        }


        //FIN ELIMINAR EN GRID







    }
}