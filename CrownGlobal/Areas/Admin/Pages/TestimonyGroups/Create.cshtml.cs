using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrownGlobal.Data;
using CrownGlobal.ViewModels.TestimonyGroup;
using CrownGlobal.Interfaces;

namespace CrownGlobal.Areas.Admin.Pages.TestimonyGroups
{
    public class CreateModel : PageModel
    {
        private readonly ITestimonyGroupService _testimonyGroupService;

        public CreateModel(ITestimonyGroupService testimonyGroupService)
        {
            _testimonyGroupService = testimonyGroupService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TestimonyGroupPostVM TestimonyGroup { get; set; }

        public string Error { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _testimonyGroupService.Add(TestimonyGroup);

            if (result.HasError)
            {
                Error = string.Join("\n", result.ErrorMessages);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
