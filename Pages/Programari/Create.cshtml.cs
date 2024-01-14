using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Programari
{
    public class CreateModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;
        public List<SelectListItem> BisericaOptions { get; set; }

        public List<SelectListItem> ServiciiOptions { get; set; }

        public List<SelectListItem> PreotOptions { get; set; }
        public List<SelectListItem> EnoriasOptions { get; set; }




        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

            public IActionResult OnGet()
            {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var currentEnorias = _context.Enorias
                              .Where(e => e.Email == userEmail)
                              .Select(e => new SelectListItem
                              {
                                  Value = e.ID.ToString(),
                                  Text = e.FullName
                              }).ToList();

            ViewData["BisericaID"] = new SelectList(_context.Biserica, "ID", "Nume");
            ViewData["EnoriasID"] = new SelectList(currentEnorias, "Value", "Text");
            ViewData["PreotID"] = new SelectList(_context.Preot, "ID", "FullName");
            ViewData["ServiciuID"] = new SelectList(_context.Serviciu, "ID", "NumeServiciu");
            return Page();
            }

        [BindProperty]
        public Programare Programare { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Programare == null || Programare == null)
            {
                return Page();
            }

            _context.Programare.Add(Programare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
