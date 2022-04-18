#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ST1_ASPNETCore.Models;

namespace ST1_ASPNETCore.Data
{
    public class ST1_ASPNETCoreContext : DbContext
    {
        public ST1_ASPNETCoreContext(DbContextOptions<ST1_ASPNETCoreContext> options)
            : base(options)
        {
        }
        public DateTime Fecha { get; set; } = new DateTime(2022, 3, 20);
        public int Sucursal { get; set; } = 1;

        public DbSet<ST1_ASPNETCore.Models.Movie> Movie { get; set; }

        public DbSet<ST1_ASPNETCore.Models.TipoProducto> TipoProducto { get; set; }

        public DbSet<ST1_ASPNETCore.Models.Stock_Web> Stock_Web { get; set; }
    }
}
