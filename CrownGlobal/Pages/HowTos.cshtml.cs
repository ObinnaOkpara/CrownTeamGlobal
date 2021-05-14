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
    public class HowTosModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public HowTosModel(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<HowTo> HowTos { get; set; }

        public async Task OnGetAsync()
        {
            HowTos = await _context.HowTos.ToListAsync();
        }
    }
}
