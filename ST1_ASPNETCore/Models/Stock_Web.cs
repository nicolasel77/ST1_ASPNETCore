using System.ComponentModel.DataAnnotations;

namespace ST1_ASPNETCore.Models
{
    public class Stock_Web
    {
        public int ID { get; set; } = 0;

        /// <summary>
        /// La siguiente linea es para que solo tenga que mostrar la fecha, sin la hora
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public int Sucursal { get; set; }
        public int Producto { get; set; }
        public string? Descripcion { get; set; }
        public float Kilos { get; set; }
    }
}
