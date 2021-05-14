using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrownGlobal.Data;
using CrownGlobal.Interfaces;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CrownGlobal.Areas.Admin.Pages.Testimonies
{
    public class CreateModel : PageModel
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        private readonly IFileStore _fileStore;

        public CreateModel(ApplicationDbContext context, IFileStore fileStore)
        {
            _context = context;
            _fileStore = fileStore;
        }

        public IActionResult OnGet()
        {
        ViewData["TestimonyGroups"] = new SelectList(_context.TestimonyGroups, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Testimony Testimony { get; set; }
        [BindProperty]
        [Required]
        public IFormFile Video { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (!_fileStore.ValidateVideoFile(Video, out var error))
                {
                    ModelState.AddModelError(string.Empty, error);
                    return Page();
                }

                var fileName = await _fileStore.UploadFileAsync(Video);

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    ModelState.AddModelError(string.Empty, "An error occured while uploading file.");
                    return Page();
                }

                Testimony.Video = fileName;

                _context.Testimonies.Add(Testimony);
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
