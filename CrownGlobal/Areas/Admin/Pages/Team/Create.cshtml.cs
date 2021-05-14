using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrownGlobal.Data;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using CrownGlobal.Interfaces;

namespace CrownGlobal.Areas.Admin.Pages.Team
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStore _fileStore;

        public CreateModel(ApplicationDbContext context, IFileStore fileStore)
        {
            _context = context;
            _fileStore = fileStore;
        }

        public IActionResult OnGet()
        {
        ViewData["Ranks"] = new SelectList(_context.Ranks.OrderBy(m=>m.Order), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public TeamMember TeamMember { get; set; }
        [BindProperty]
        [Required]
        public IFormFile Picture { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


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

                TeamMember.Image = fileName;

                _context.TeamMembers.Add(TeamMember);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
