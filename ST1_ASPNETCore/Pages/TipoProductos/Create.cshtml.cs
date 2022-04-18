#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ST1_ASPNETCore.Data;
using ST1_ASPNETCore.Models;

namespace ST1_ASPNETCore.Pages.TipoProductos
{
    public class CreateModel : PageModel
    {
        private readonly ST1_ASPNETCore.Data.ST1_ASPNETCoreContext _context;

        public CreateModel(ST1_ASPNETCore.Data.ST1_ASPNETCoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TipoProducto TipoProducto { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TipoProducto.Add(TipoProducto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
