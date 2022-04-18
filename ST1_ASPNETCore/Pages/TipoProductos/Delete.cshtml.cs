#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ST1_ASPNETCore.Data;
using ST1_ASPNETCore.Models;

namespace ST1_ASPNETCore.Pages.TipoProductos
{
    public class DeleteModel : PageModel
    {
        private readonly ST1_ASPNETCore.Data.ST1_ASPNETCoreContext _context;

        public DeleteModel(ST1_ASPNETCore.Data.ST1_ASPNETCoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TipoProducto TipoProducto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TipoProducto = await _context.TipoProducto.FirstOrDefaultAsync(m => m.ID == id);

            if (TipoProducto == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TipoProducto = await _context.TipoProducto.FindAsync(id);

            if (TipoProducto != null)
            {
                _context.TipoProducto.Remove(TipoProducto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
