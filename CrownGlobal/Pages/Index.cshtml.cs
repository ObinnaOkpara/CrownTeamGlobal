using CrownGlobal.ViewModels.Testimony;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public IndexModel(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TestimonyVM> Testimonies { get; set; }
        public List<string> TestimonyGroup { get; set; }

        public async Task OnGet()
        {
            Testimonies = await _context.Testimonies.OrderByDescending(m=>m.Id).Take(3)
                   .Select(m => new TestimonyVM()
                   {
                       TestimonyGroup = m.TestimonyGroup.Name,
                       Video = m.Video,
                       Title = m.Title,
                   }).ToListAsync();

            TestimonyGroup = Testimonies.Select(m => m.TestimonyGroup).Distinct().ToList();
        }

    }
}
