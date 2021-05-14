using CrownGlobal.Interfaces;
using CrownGlobal.ViewModels.Testimony;
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
    public class TestimonyController : ControllerBase
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public TestimonyController(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var data = await _context.TestimonyGroups.ToListAsync();

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Testimonies
                .Select(m => new TestimonyVM()
                {
                    TestimonyGroup = m.TestimonyGroup.Name,
                    Video = m.Video,
                    Title = m.Title,
                }).ToListAsync();

            return Ok(data);
        }
    }

}
