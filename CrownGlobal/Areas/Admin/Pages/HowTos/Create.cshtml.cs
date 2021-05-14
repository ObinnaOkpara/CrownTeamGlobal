using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrownGlobal.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using CrownGlobal.Interfaces;

namespace CrownGlobal.Areas.Admin.Pages.HowTos
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
            return Page();
        }

        [BindProperty]
        public HowTo HowTo { get; set; }
        [BindProperty]
        [Required]
        public IFormFile File { get; set; }

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
                var error = "";
                if (!_fileStore.ValidateImageFile(File, out error) && !_fileStore.ValidateVideoFile(File, out error))
                {
                    ModelState.AddModelError(string.Empty, "Invalid file type");
                    return Page();
                }

                var fileName = await _fileStore.UploadFileAsync(File);

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    ModelState.AddModelError(string.Empty, "An error occured while uploading file.");
                    return Page();
                }

                HowTo.File = fileName;

                _context.HowTos.Add(HowTo);
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
