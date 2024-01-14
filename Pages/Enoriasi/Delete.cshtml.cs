using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Enoriasi
{
    public class DeleteModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public DeleteModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Enorias Enorias { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Enorias == null)
            {
                return NotFound();
            }

            var enorias = await _context.Enorias.FirstOrDefaultAsync(m => m.ID == id);

            if (enorias == null)
            {
                return NotFound();
            }
            else 
            {
                Enorias = enorias;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Enorias == null)
            {
                return NotFound();
            }
            var enorias = await _context.Enorias.FindAsync(id);

            if (enorias != null)
            {
                Enorias = enorias;
                _context.Enorias.Remove(Enorias);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
