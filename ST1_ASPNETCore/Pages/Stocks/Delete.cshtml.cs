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

namespace ST1_ASPNETCore.Pages.Stocks
{
    public class DeleteModel : PageModel
    {
        private readonly ST1_ASPNETCore.Data.ST1_ASPNETCoreContext _context;

        public DeleteModel(ST1_ASPNETCore.Data.ST1_ASPNETCoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Stock_Web Stock { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Stock = await _context.Stock_Web.FirstOrDefaultAsync(m => m.ID == id);

            if (Stock == null)
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

            Stock = await _context.Stock_Web.FindAsync(id);

            if (Stock != null)
            {
                _context.Stock_Web.Remove(Stock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
