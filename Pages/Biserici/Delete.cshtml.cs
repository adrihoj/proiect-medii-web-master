using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Biserici
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public DeleteModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Biserica Biserica { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Biserica == null)
            {
                return NotFound();
            }

            var biserica = await _context.Biserica.FirstOrDefaultAsync(m => m.ID == id);

            if (biserica == null)
            {
                return NotFound();
            }
            else 
            {
                Biserica = biserica;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Biserica == null)
            {
                return NotFound();
            }
            var biserica = await _context.Biserica.FindAsync(id);

            if (biserica != null)
            {
                Biserica = biserica;
                _context.Biserica.Remove(Biserica);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
