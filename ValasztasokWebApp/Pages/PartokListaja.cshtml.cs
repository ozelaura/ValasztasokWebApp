using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValasztasokWebApp.Models;

namespace ValasztasokWebApp.Pages
{
    public class PartokListajaModel : PageModel
    {
        private readonly ValasztasokWebApp.Models.ValasztasDbContext _context;

        public PartokListajaModel(ValasztasokWebApp.Models.ValasztasDbContext context)
        {
            _context = context;
        }

        public IList<Part> Part { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Part = await _context.Partok.ToListAsync();
        }
    }
}
