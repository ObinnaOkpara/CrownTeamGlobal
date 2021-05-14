using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrownGlobal.Data;

namespace CrownGlobal.Areas.Admin.Pages.Testimonies
{
    public class DetailsModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public DetailsModel(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
