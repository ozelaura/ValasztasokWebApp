using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValasztasokWebApp.Models;

namespace ValasztasokWebApp.Pages
{
    public class PartEditModel : PageModel
    {
        private readonly ValasztasokWebApp.Models.ValasztasDbContext _context;

        public PartEditModel(ValasztasokWebApp.Models.ValasztasDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Part Part { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part =  await _context.Partok.FirstOrDefaultAsync(m => m.RovidNev == id);
            if (part == null)
            {
                return NotFound();
            }
            Part = part;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            _context.Attach(Part).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartExists(Part.RovidNev))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PartExists(string id)
        {
            return _context.Partok.Any(e => e.RovidNev == id);
        }
    }
}
