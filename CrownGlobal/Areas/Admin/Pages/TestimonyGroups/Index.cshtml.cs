using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrownGlobal.Data;
using CrownGlobal.Interfaces;
using CrownGlobal.ViewModels.TestimonyGroup;

namespace CrownGlobal.Areas.Admin.Pages.TestimonyGroups
{
    public class IndexModel : PageModel
    {
        private readonly ITestimonyGroupService _testimonyGroupService;

        public IndexModel(ITestimonyGroupService testimonyGroupService)
        {
            _testimonyGroupService = testimonyGroupService;
        }

        public IList<TestimonyGroupListVM> TestimonyGroups { get;set; }

        public async Task OnGetAsync()
        {
            TestimonyGroups = (await _testimonyGroupService.GetAll()).Data;
        }
    }
}
