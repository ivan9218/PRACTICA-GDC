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
    public class EmpleadosController : Controller
    {

        private DISETOPEntities db = new DISETOPEntities();

        // GET: Empleados
        public ActionResult Index()
        {
            return View();
        }

        //INICIO DE LISTAR EN GRID
        public ActionResult VistaEmpleados()
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

            return View(empleados);
        }


        //FIN DE LISTAR EN GRID

        //INICIO DE INSERTAR EN GRID

        [HttpGet]
        public ActionResult InsertarEmpleado()
        {
            return View(); // Esta vista mostrará el formulario de inserción
        }

        [HttpPost]
        public ActionResult InsertarEmpleado(EMPLEADO empleado)
        {
            // INICIO VALIDACIONES
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(empleado.CODIGO_EMPLEADO))
                {
                    return Json(new { success = false, message = "El campo 'Codigo de Empleado' es obligatorio." });
                }
                if (string.IsNullOrWhiteSpace(empleado.NOMBRE_EMPLEADO))
                {
                    return Json(new { success = false, message = "El campo 'Nombre de Empleado' es obligatorio." });
                }
                if (empleado.CEDULA == null)
                {
                    return Json(new { success = false, message = "El campo 'Cedula' es obligatorio." });
                    ////empleado.CEDULA = 0; // Asigna un valor predeterminado de 0
                }
                // Asigna 0 a telefono si es nulo o está vacío
                if (empleado.TELEFONO == null || empleado.TELEFONO == 0)
                {
                    empleado.TELEFONO = 0;
                }

                // Asigna cadena vacía a comentario si está nulo
                if (string.IsNullOrWhiteSpace(empleado.COMENTARIO))
                {
                    empleado.COMENTARIO = string.Empty;
                }
                // Asigna cadena vacía a comentario si está nulo
                if (string.IsNullOrWhiteSpace(empleado.DIRECCION))
                {
                    empleado.DIRECCION = string.Empty;
                }
                // Asigna cadena vacía a comentario si está nulo
                if (string.IsNullOrWhiteSpace(empleado.CORREO))
                {
                    empleado.CORREO = string.Empty;
                }

                // Inserta el nuevo empleado en la base de datos utilizando el procedimiento almacenado
                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    var empleadoExistente = context.EMPLEADOS.FirstOrDefault(e => e.CODIGO_EMPLEADO == empleado.CODIGO_EMPLEADO);

                    if (empleadoExistente != null)
                    {
                        return Json(new { success = false, message = "El empleado con el código " + empleado.CODIGO_EMPLEADO + " ya existe." });
                    }

                    var empleadoExistentePorCedula = context.EMPLEADOS.FirstOrDefault(e => e.CEDULA == empleado.CEDULA);

                    if (empleadoExistentePorCedula != null)
                    {
                        return Json(new { success = false, message = "El empleado con la cédula " + empleado.CEDULA + " ya existe." });
                    }
                    // FIN VALIDACIONES

                    context.Database.ExecuteSqlCommand("sp_InsertaEmpleados @codigo_empleado, @cedula, @nombre_empleado, @telefono, @correo, @direccion, @comentario",
                        new SqlParameter("@codigo_empleado", empleado.CODIGO_EMPLEADO),
                        new SqlParameter("@cedula", empleado.CEDULA),
                        new SqlParameter("@nombre_empleado", empleado.NOMBRE_EMPLEADO),
                        new SqlParameter("@telefono", empleado.TELEFONO),
                        new SqlParameter("@correo", empleado.CORREO),
                        new SqlParameter("@direccion", empleado.DIRECCION),
                        new SqlParameter("@comentario", empleado.COMENTARIO)
                    );
                }

                return Json(new { success = true });
            }

            return View(empleado);
        }


        //FIN DE INSERTAR EN GRID


        //INICIO DE EDITAR EN GRID

        [HttpGet]
        public ActionResult EditarEmpleado(string codigo_empleado)
        {
            // Recuperar el empleado de la base de datos usando el código_empleado proporcionado
            using (var context = new DISETOP.Models.DISETOPEntities())
            {
                EMPLEADO empleado = context.EMPLEADOS.FirstOrDefault(e => e.CODIGO_EMPLEADO == codigo_empleado);

                if (empleado != null)
                {
                    return View(empleado); // Mostrar la vista de edición con los datos del empleado
                }
                else
                {
                    return HttpNotFound(); // Devolver un error 404 si no se encuentra el empleado
                }
            }
        }



        [HttpPost]
        public ActionResult EditarEmpleado(EMPLEADO empleado)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DISETOP.Models.DISETOPEntities())
                {
                    // INICIO VALIDACIONES
                    // Verifica si la cédula actual ya existe en otro empleado (excluyendo al empleado actual)
                    var empleadoExistente = context.EMPLEADOS.FirstOrDefault(e => e.CEDULA == empleado.CEDULA && e.CODIGO_EMPLEADO != empleado.CODIGO_EMPLEADO);

                    if (empleadoExistente != null)
                    {
                        return Json(new { success = false, message = "Ya existe otro empleado con la misma cédula." });
                    }
                   
                    if (string.IsNullOrWhiteSpace(empleado.NOMBRE_EMPLEADO))
                    {
                        return Json(new { success = false, message = "El campo 'Nombre de Empleado' es obligatorio." });
                    }
                    if (string.IsNullOrWhiteSpace(empleado.CODIGO_EMPLEADO))
                    {
                        return Json(new { success = false, message = "El campo 'Codigo de Empleado' es obligatorio." });
                    }
                    if (empleado.CEDULA == null)
                    {
                        return Json(new { success = false, message = "El campo 'Cedula' es obligatorio." });
                        ////empleado.CEDULA = 0; // Asigna un valor predeterminado de 0
                    }

                    // Asigna 0 a telefono si es nulo o está vacío
                    if (empleado.TELEFONO == null || empleado.TELEFONO == 0)
                    {
                        empleado.TELEFONO = 0;
                    }
                   
                    // Asigna cadena vacía a comentario si está nulo
                    if (string.IsNullOrWhiteSpace(empleado.COMENTARIO))
                    {
                        empleado.COMENTARIO = string.Empty;
                    }

                    // Asigna cadena vacía a dirección si está nulo
                    if (string.IsNullOrWhiteSpace(empleado.DIRECCION))
                    {
                        empleado.DIRECCION = string.Empty;
                    }

                    // Asigna cadena vacía a correo si está nulo
                    if (string.IsNullOrWhiteSpace(empleado.CORREO))
                    {
                        empleado.CORREO = string.Empty;
                    }
                    // FIN VALIDACIONES
                    // Actualiza el empleado en la base de datos utilizando el procedimiento almacenado
                    context.Database.ExecuteSqlCommand("sp_ActualizaEmpleados @codigo_empleado, @cedula, @nombre_empleado, @telefono, @correo, @direccion, @comentario",
                        new SqlParameter("@codigo_empleado", empleado.CODIGO_EMPLEADO),
                        new SqlParameter("@cedula", empleado.CEDULA),
                        new SqlParameter("@nombre_empleado", empleado.NOMBRE_EMPLEADO),
                        new SqlParameter("@telefono", empleado.TELEFONO),
                        new SqlParameter("@correo", empleado.CORREO),
                        new SqlParameter("@direccion", empleado.DIRECCION),
                        new SqlParameter("@comentario", empleado.COMENTARIO)
                    );

                    return Json(new { success = true });
                }
            }

            return Json(new { success = false, message = "Error al actualizar el empleado." });
        }


        //FIN DE EDITAR EN GRID

        //INICIO ELIMINAR EN GRID
        public ActionResult EliminarEmpleado(string codigoEmpleado)
        {
            // Llama al procedimiento almacenado para eliminar el empleado
            using (var context = new DISETOP.Models.DISETOPEntities()) 
            {
                context.Database.ExecuteSqlCommand("exec sp_EliminaEmpleados @codigo_empleado", new SqlParameter("@codigo_empleado", codigoEmpleado));
            }

           
            return RedirectToAction("VistaEmpleados");
        }


        //FIN ELIMINAR EN GRID

    }
}