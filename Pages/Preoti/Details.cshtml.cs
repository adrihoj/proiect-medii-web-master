using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Preoti
{
    public class DetailsModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public DetailsModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

      public Preot Preot { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Preot == null)
            {
                return NotFound();
            }

            var preot = await _context.Preot.FirstOrDefaultAsync(m => m.ID == id);
            if (preot == null)
            {
                return NotFound();
            }
            else 
            {
                Preot = preot;
            }
            return Page();
        }
    }
}
