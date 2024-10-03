using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
                Path.Combine(_env.ContentRootPath, "Uploads", UploadFile.FileName);

            using (var stream = new FileStream(UploadfilePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }
            //adatbázisba töltés

            StreamReader sr = new StreamReader(UploadfilePath);
            sr.ReadLine();
            List<Part> partok = new List<Part>();
            
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var elemek = sor.Split();
                Jelolt ujJelolt = new Jelolt();
                ujJelolt.Kerulet = int.Parse(elemek[0]);
                ujJelolt.Szavazatokszama = int.Parse(elemek[1]);
                ujJelolt.Nev = elemek[2] + " " + elemek[3] ;
                ujJelolt.PartRovidNev = elemek[4];
                _context.Jeloltek.Add(ujJelolt);
                if(!partok.Select(x => x.RovidNev).Contains(elemek[4]))
                {
                    _context.Partok.Add(new Part { RovidNev = elemek[4] });
                    partok.Add(new Part { RovidNev = elemek[4] });
                }
                _context.Jeloltek.Add(ujJelolt);
            }
            sr.Close();
            _context.SaveChanges();
            return Page();
        }
    }
}
