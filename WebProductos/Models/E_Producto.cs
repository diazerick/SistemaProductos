using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProductos.Models
{
    public class E_Producto
    {
        //Propiedades simple
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Disponible { get; set; }
        public int Proveedor { get; set; }
        public int Cantidad { get; set; }

        //Propiedad de solo lectura
        public string DisponibleDescripcion
        {
            get
            {
                if (Disponible == true)
                    return "Si";
                else
                    return "No";
            }
        }

        public DateTime FechaCaducidad
        {
            get
            {
                return FechaIngreso.AddMonths(2);
            }
        }
    }
}