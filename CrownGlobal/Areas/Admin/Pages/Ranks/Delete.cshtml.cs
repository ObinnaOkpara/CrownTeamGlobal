using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrownGlobal.Data;

namespace CrownGlobal.Areas.Admin.Pages.Ranks
{
    public class DeleteModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public DeleteModel(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rank Rank { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rank = await _context.Ranks.FirstOrDefaultAsync(m => m.Id == id);

            if (Rank == null)
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

            Rank = await _context.Ranks.FindAsync(id);

            if (Rank != null)
            {
                _context.Ranks.Remove(Rank);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
