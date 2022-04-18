#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ST1_ASPNETCore.Models;
using System.Data;

namespace ST1_ASPNETCore.Pages.Stocks
{
    public class EditModel : PageModel
    {
        private readonly ST1_ASPNETCore.Data.ST1_ASPNETCoreContext _context;

        public EditModel(ST1_ASPNETCore.Data.ST1_ASPNETCoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Stock_Web Stock { get; set; }

        public async Task<IActionResult> OnGetAsync(int? prod)
        {
            if (prod == null)
            {
                return NotFound();
            }            

            Stock = stock(prod);

            if (Stock == null)
            {
                return NotFound();
            }
            return Page();
        }

        public Stock_Web stock(int? prod)
        {
            using (SqlConnection oConexion = new SqlConnection("Data Source=192.168.1.11;Initial Catalog=dbDatos2;User Id=Nikorasu;Password=Oficina02"))
            {
                SqlCommand cmd = new SqlCommand($"SELECT TOP 1 ID, Nombre AS Descripcion" +
                $", ISNULL((SELECT Kilos FROM Stock WHERE Id_Sucursales={_context.Sucursal} AND Fecha='{_context.Fecha:MM/dd/yyyy}' AND Id_Productos=Productos.Id), 0) AS Kilos" +
                $", {_context.Sucursal} AS ID_Sucursales, CONVERT(DATETIME, '{_context.Fecha:MM/dd/yyyy}') AS Fecha" +
                $", ISNULL((SELECT ID FROM Stock WHERE Id_Sucursales={_context.Sucursal} AND Fecha='{_context.Fecha:MM/dd/yyyy}' AND Id_Productos=Productos.Id), 0) " +
                $"AS ID_Stock" +
                $" FROM Productos WHERE Id={prod} AND Ver = 1 ORDER BY Id_Tipo, Id", oConexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        return new Stock_Web
                        {
                            ID = Convert.ToInt32(dr["ID_Stock"]),
                            Fecha = Convert.ToDateTime(dr["Fecha"].ToString()),
                            Sucursal = Convert.ToInt32(dr["ID_Sucursales"]),
                            Producto = Convert.ToInt32(dr["ID"]),
                            Descripcion = dr["Descripcion"].ToString(),
                            Kilos = Convert.ToSingle(dr["Kilos"])

                        };
                        
                    }
                    dr.Close();                    

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (Stock.ID == 0)
            {
                Agregar();
            }
            else
            {
                Actualizar();
            }
            
            return RedirectToPage("./Index");
        }

        private void Agregar()
        {
            using (SqlConnection oConexion = new SqlConnection("Data Source=192.168.1.11;Initial Catalog=dbDatos2;User Id=Nikorasu;Password=Oficina02"))
            {
                string cadena = $"INSERT INTO STOCK (Fecha, Id_Sucursales, Id_Productos, Descripcion, Costo, Kilos)VALUES(" +
                    $"'{Stock.Fecha:MM/dd/yyyy}'" +
                    $",{Stock.Sucursal}" +
                    $",{Stock.Producto}" +
                    $",'{Stock.Descripcion}'" +
                    $",0" +
                    $",{Stock.Kilos.ToString().Replace(",", ".")})";
                SqlCommand cmd = new SqlCommand(cadena, oConexion);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                cmd.ExecuteNonQuery();

            }
        }

        private void Actualizar()
        {
            using (SqlConnection oConexion = new SqlConnection("Data Source=192.168.1.11;Initial Catalog=dbDatos2;User Id=Nikorasu;Password=Oficina02"))
            {
                string cadena = $"UPDATE STOCK SET Kilos ={Stock.Kilos.ToString().Replace(",", ".")} WHERE ID={Stock.ID}";
                SqlCommand cmd = new SqlCommand(cadena, oConexion);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                cmd.ExecuteNonQuery();

            }
        }

        private bool StockExists(int id)
        {
            return _context.Stock_Web.Any(e => e.Producto == id);
        }
    }
}
