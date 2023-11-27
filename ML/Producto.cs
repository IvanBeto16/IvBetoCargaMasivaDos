using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string FechaRegistro { get; set; }
    }
}
