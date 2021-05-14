using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrownGlobal.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CrownGlobal.Areas.Admin.Pages.Testimonies
{
    public class DeleteModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteModel(CrownGlobal.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Testimony Testimony { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Testimony = await _context.Testimonies
                .Include(t => t.TestimonyGroup).FirstOrDefaultAsync(m => m.Id == id);

            if (Testimony == null)
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

            Testimony = await _context.Testimonies.FindAsync(id);

            if (Testimony != null)
            {
                var path = Testimony.Video;

                _context.Testimonies.Remove(Testimony);
                await _context.SaveChangesAsync();
                var rootFolder = _env.WebRootPath;

                if (System.IO.File.Exists(Path.Combine(rootFolder, path)))
                {
                    System.IO.File.Delete(Path.Combine(rootFolder, Testimony.Video));
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
