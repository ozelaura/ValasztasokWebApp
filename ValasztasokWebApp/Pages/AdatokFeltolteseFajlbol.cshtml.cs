using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValasztasokWebApp.Models;

namespace ValasztasokWebApp.Pages
{
    public class AdatokFeltolteseFajlbolModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly ValasztasDbContext _context;
        public AdatokFeltolteseFajlbolModel(IWebHostEnvironment env, 
            ValasztasDbContext context)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }
        
        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            //Fájl feltöltése
            var UploadfilePath =
                Path.Combine(_env.ContentRootPath, "uploads", UploadFile.FileName);

            using (var stream = new FileStream(UploadfilePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }
            //adatbázisba töltés

            StreamReader sr = new StreamReader(UploadfilePath);
            //...
            sr.Close();


            return Page();
        }
    }
}
