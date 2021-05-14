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

namespace CrownGlobal.Areas.Admin.Pages.Team
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
        public TeamMember TeamMember { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamMember = await _context.TeamMembers
                .Include(t => t.Rank).FirstOrDefaultAsync(m => m.Id == id);

            if (TeamMember == null)
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

            TeamMember = await _context.TeamMembers.FindAsync(id);

            if (TeamMember != null)
            {
                var path = TeamMember.Image;

                _context.TeamMembers.Remove(TeamMember);
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
