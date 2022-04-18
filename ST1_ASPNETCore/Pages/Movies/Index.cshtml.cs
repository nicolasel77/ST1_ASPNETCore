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

namespace ST1_ASPNETCore.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly ST1_ASPNETCore.Data.ST1_ASPNETCoreContext _context;

        public IndexModel(ST1_ASPNETCore.Data.ST1_ASPNETCoreContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
