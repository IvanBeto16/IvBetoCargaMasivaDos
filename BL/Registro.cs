using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Registro
    {
        public static ML.Result AddRegistro(ML.Registro registro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string cmd = "INSERT INTO Registro VALUES(@Nombre,@PrecioUnitario,@Stock,@Descripcion,@FechaRegistro,@IdProveedor,@Proveedor,@Numero,@Direccion)";
                    SqlCommand query = new SqlCommand(cmd, conexion);

                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    param[0].Value = registro.NombreProducto;
                    param[1] = new SqlParameter("@PrecioUnitario", SqlDbType.VarChar);
                    param[1].Value = registro.PrecioUnitario;
                    param[2] = new SqlParameter("@Stock", SqlDbType.Int);
                    param[2].Value = registro.Stock;
                    param[3] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    param[3].Value = registro.Descripcion;
                    param[4] = new SqlParameter("@FechaIngreso", SqlDbType.VarChar);
                    param[4].Value = registro.FechaRegistro;
                    param[5] = new SqlParameter("@IdProveedor", SqlDbType.Int);
                    param[5].Value = registro.IdProveedor;
                    param[6] = new SqlParameter("@Proveedor", SqlDbType.VarChar);
                    param[6].Value = registro.NombreProveedor;
                    param[7] = new SqlParameter("@Numero", SqlDbType.Int);
                    param[7].Value = registro.Numero;
                    param[8] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    param[8].Value = registro.Direccion;

                    query.Parameters.AddRange(param);
                    query.Connection.Open();

                    int inserciones = query.ExecuteNonQuery();
                    if (inserciones > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error durante la insercion";
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
                result.Ex = ex;
            }
            return result;
        }

    }
}
