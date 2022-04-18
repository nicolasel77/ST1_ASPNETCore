using System.ComponentModel.DataAnnotations;

namespace ST1_ASPNETCore.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// La siguiente linea es para que solo tenga que mostrar la fecha, sin la hora
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
