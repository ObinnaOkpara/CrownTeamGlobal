﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrownGlobal.Data;

namespace CrownGlobal.Areas.Admin.Pages.Ranks
{
    public class IndexModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public IndexModel(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Rank> Rank { get;set; }

        public async Task OnGetAsync()
        {
            Rank = await _context.Ranks.ToListAsync();
        }
    }
}
