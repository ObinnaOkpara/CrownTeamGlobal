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
using Microsoft.AspNetCore.Http;

namespace CrownGlobal.Areas.Admin.Pages.Team
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStore _fileStore;

        public EditModel(ApplicationDbContext context, IFileStore fileStore)
        {
            _context = context;
            _fileStore = fileStore;
        }


        [BindProperty]
        public TeamMember TeamMember { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }


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
           ViewData["RankId"] = new SelectList(_context.Ranks, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var tm = await _context.TeamMembers.FirstOrDefaultAsync(m => m.Id == TeamMember.Id);
            if (tm == null)
            {
                return NotFound();
            }

            if (Picture != null)
            {
                try
                {
                    if (!_fileStore.ValidateImageFile(Picture, out var error))
                    {
                        ModelState.AddModelError(string.Empty, error);
                        return Page();
                    }

                    var fileName = await _fileStore.UploadFileAsync(Picture);

                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while uploading file.");
                        return Page();
                    }

                    tm.Image = fileName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return Page();
                }

            }

            tm.Location = TeamMember.Location;
            tm.Name = TeamMember.Name;
            tm.RankId = TeamMember.RankId;
            tm.Story = TeamMember.Story;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occured while saving file.");
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
