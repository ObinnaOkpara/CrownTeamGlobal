using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrownGlobal.Data;

namespace CrownGlobal.Areas.Admin.Pages.Ranks
{
    public class EditModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public EditModel(CrownGlobal.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Rank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RankExists(Rank.Id))
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

        private bool RankExists(int id)
        {
            return _context.Ranks.Any(e => e.Id == id);
        }
    }
}
