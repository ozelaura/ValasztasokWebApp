using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValasztasokWebApp.Models;

namespace ValasztasokWebApp.Pages
{
    public class UjPartModel : PageModel
    {
        private readonly ValasztasokWebApp.Models.ValasztasDbContext _context;

        public UjPartModel(ValasztasokWebApp.Models.ValasztasDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Part Part { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            _context.Partok.Add(Part);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
