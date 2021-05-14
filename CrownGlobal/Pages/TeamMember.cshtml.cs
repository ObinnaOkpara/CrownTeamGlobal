using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrownGlobal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CrownGlobal.Pages
{
    public class TeamMemberModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public TeamMemberModel(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
