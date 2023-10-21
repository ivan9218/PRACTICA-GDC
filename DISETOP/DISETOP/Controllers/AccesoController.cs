using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using DISETOP.Models;

using System.Data.SqlClient;
using System.Data;

namespace DISETOP.Controllers
{
    public class AccesoController : Controller
    {

        //static string cadena = "Data Source=DESKTOP-L0GVN2O\\SQLEXPRESS;Initial Catalog=EJEMPLO;Integrated Security=true";
        static string cadena = "Data Source=DESKTOP-F5BQ241\\SQLEXPRESS;Initial Catalog=DISETOP;Integrated Security=true";



        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckPIN(string pin)
        {
            const string correctPIN = "0160"; // PIN QUEMADO

            if (pin == correctPIN)
            {
                // El PIN es correcto
                return Json(new { success = true });
            }
            else
            {
                // El PIN no es válido
                return Json(new { success = false });
            }




        }




        [HttpPost]
        public ActionResult Registrar(Usuarios oUsuario)
        {
            bool registrado;
            string mensaje;

            if (oUsuario.Contrasena == oUsuario.ConfirmarClave)
            {

                oUsuario.Contrasena = ConvertirSha256(oUsuario.Contrasena);
            }
            else {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena)) {

                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Usuario", oUsuario.Usuario);
                cmd.Parameters.AddWithValue("Contrasena", oUsuario.Contrasena);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();


            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(Usuarios oUsuario)
        {
            oUsuario.Contrasena = ConvertirSha256(oUsuario.Contrasena);

            using (SqlConnection cn = new SqlConnection(cadena))
            {

                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Usuario", oUsuario.Usuario);
                cmd.Parameters.AddWithValue("Contrasena", oUsuario.Contrasena);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.id_Usuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if (oUsuario.id_Usuario != 0)
            {

                Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "Home");
            }
            else {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }

           
        }



        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }



    }
}