using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult CargaMasiva()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LeerArchivo()
        {
            ML.Result result = new ML.Result();
            HttpPostedFileBase file = Request.Files["ArchivoComas"];

            if(file != null )
            {
                string extension = Path.GetExtension(file.FileName);
                string extensionValida = ConfigurationManager.AppSettings["TipoArchivo"];
                if(extension == extensionValida)
                {
                    ML.Registro registro = new ML.Registro();
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                        {
                            string query = "SELECT * FROM [RegistrosBullCopy$]";
                            char separador = ',';
                            string linea;
                            StreamReader reader = new StreamReader(file.InputStream);

                            reader.ReadLine(); //Para leer y saltar la primera linea del encabezado
                            while((linea =  reader.ReadLine()) != null)
                            {
                                string[] row = linea.Split(separador);
                                registro.NombreProducto = row[0];
                                registro.PrecioUnitario = row[1];
                                registro.Stock = int.Parse(row[2]);
                                registro.Descripcion = row[3];
                                registro.FechaRegistro = row[4]; //Excepcion de formato de datos.
                                registro.IdProveedor = int.Parse(row[6]);
                                registro.NombreProveedor = row[7];
                            }
                        }
                    }catch(Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        result.Ex = ex;
                    }
                }
                ViewBag.Message = result.ErrorMessage;
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
            }
            return RedirectToAction("CargaMasiva", "CargaMasiva");
        }
    }
}