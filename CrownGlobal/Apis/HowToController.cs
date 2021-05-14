using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class HowToController : ControllerBase
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public HowToController(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSome()
        {
            var data = await _context.HowTos.OrderByDescending(m => m.Id)
                .Take(6).ToListAsync();

            return Ok(data);
        }
    }
}
