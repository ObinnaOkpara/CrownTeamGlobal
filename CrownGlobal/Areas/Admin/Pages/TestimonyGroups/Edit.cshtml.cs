using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrownGlobal.Data;
using CrownGlobal.Interfaces;
using CrownGlobal.ViewModels.TestimonyGroup;

namespace CrownGlobal.Areas.Admin.Pages.TestimonyGroups
{
    public class EditModel : PageModel
    {
        private readonly ITestimonyGroupService _testimonyGroupService;

        public EditModel(ITestimonyGroupService testimonyGroupService)
        {
            _testimonyGroupService = testimonyGroupService;
        }

        [BindProperty]
        public TestimonyGroupPostVM TestimonyGroup { get; set; }
        [BindProperty]
        public int Id { get; set; }

        public string Error { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _testimonyGroupService.Get(id.Value);

            if (result.HasError)
            {
                return NotFound();
            }
            else
            {
                Id = id.Value;
                TestimonyGroup = new TestimonyGroupPostVM() { Name = result.Data.Name };
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var result = await _testimonyGroupService.Update(Id, TestimonyGroup);
                if (result.HasError)
                {
                    Error = string.Join("\n", result.ErrorMessages);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
