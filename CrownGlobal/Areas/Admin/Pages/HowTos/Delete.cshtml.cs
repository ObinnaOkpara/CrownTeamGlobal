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

namespace CrownGlobal.Areas.Admin.Pages.HowTos
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
        public HowTo HowTo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HowTo = await _context.HowTos.FirstOrDefaultAsync(m => m.Id == id);

            if (HowTo == null)
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

            HowTo = await _context.HowTos.FindAsync(id);

            if (HowTo != null)
            {
                var path = HowTo.File;

                _context.HowTos.Remove(HowTo);
                await _context.SaveChangesAsync();

                var rootFolder = _env.WebRootPath;
                if (System.IO.File.Exists(Path.Combine(rootFolder, path)))
                {
                    System.IO.File.Delete(Path.Combine(rootFolder, path));
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
