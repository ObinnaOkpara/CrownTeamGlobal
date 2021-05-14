using CrownGlobal.Data;
using CrownGlobal.ViewModels.TeamMember;
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
    public class TeamMemberController : ControllerBase
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public TeamMemberController(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.TeamMembers.OrderBy(m=> m.Rank.Order)
                .Select(m=> new  {
                    Image = m.Image,
                    Location = m.Location,
                    Name = m.Name,
                    RankName = m.Rank.Name,
                    Id = m.Id
                }).ToListAsync();

            return Ok(data);
        }
    }
}
