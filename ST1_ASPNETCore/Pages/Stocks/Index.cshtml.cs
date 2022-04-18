#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ST1_ASPNETCore.Models;
using System.Data;

namespace ST1_ASPNETCore.Pages.Stocks
{
    public class IndexModel : PageModel
    {
        private readonly ST1_ASPNETCore.Data.ST1_ASPNETCoreContext _context;

        public IndexModel(ST1_ASPNETCore.Data.ST1_ASPNETCoreContext context)
        {
            _context = context;
        }

        public IList<Stock_Web> Stock_Web { get; set; }

        public IList<Stock_Web> Obtener()
        {
            List<Stock_Web> rptListaStock = new();
            using (SqlConnection oConexion = new SqlConnection("Data Source=192.168.1.11;Initial Catalog=dbDatos2;User Id=Nikorasu;Password=Oficina02"))
            {                
                SqlCommand cmd = new SqlCommand($"SELECT ID, Nombre AS Descripcion" +
                $", ISNULL((SELECT Kilos FROM Stock WHERE Id_Sucursales={_context.Sucursal} AND Fecha='{_context.Fecha:MM/dd/yyyy}' AND Id_Productos=Productos.Id), 0) " +
                $"AS Kilos" +
                $", {_context.Sucursal} AS ID_Sucursales, CONVERT(DATETIME, '{_context.Fecha:MM/dd/yyyy}') AS Fecha" +
                $", ISNULL((SELECT TOP 1 ID FROM Stock WHERE Id_Sucursales={_context.Sucursal} AND Fecha='{_context.Fecha:MM/dd/yyyy}' AND Id_Productos=Productos.Id), 0) " +
                $"AS ID_Stock" +
                $" FROM Productos WHERE Ver = 1 ORDER BY Id_Tipo, Id", oConexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Stock_Web item = new()
                        {
                            ID = Convert.ToInt32(dr["ID_Stock"]),
                            Fecha = Convert.ToDateTime(dr["Fecha"].ToString()),
                            Sucursal = Convert.ToInt32(dr["ID_Sucursales"]),
                            Producto = Convert.ToInt32(dr["ID"]),
                            Descripcion = dr["Descripcion"].ToString(),
                            Kilos = Convert.ToSingle(dr["Kilos"])

                        };
                        rptListaStock.Add(item);
                    }
                    dr.Close();

                    return rptListaStock;

                }
                catch (Exception ex)
                {
                    rptListaStock = null;
                    return rptListaStock;
                }
            }
        }

        public Task OnGetAsync()
        {
            return Task.Run(() =>
                {
                    Stock_Web = Obtener();
                });
        }
        
        
        //public async Task OnGetAsync()
        //{
        //    //Stock_Web = await _context.Stock_Web.ToListAsync();
        //    Stock_Web = await Obtener()
        //}

    }
}
