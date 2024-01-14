using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;
using Stripe.Checkout;
using Stripe;
using Microsoft.Extensions.Options;
using Proiect.Migrations;
using Proiect.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Proiect.Pages.Programari
{
    public class IndexModel : PageModel
    {
        private readonly ProiectContext _context;
        private readonly StripeSettings _stripeSettings;
        public string SessionId { get; set; }

        public IndexModel(ProiectContext context, IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            _context = context;
        }

        public IList<Programare> Programari { get; set; } = default!;

        public async Task OnGetAsync()
        {

            var isAdmin = User.IsInRole("Admin");

            if (_context.Programare != null)
            {
                try
                {

                    if (isAdmin)
                    {
                        Programari = await _context.Programare
                        .Include(p => p.Biserica)
                        .Include(p => p.Enorias)
                        .Include(p => p.Preot)
                        .Include(p => p.Serviciu)
                        .ToListAsync();

                    }
                    else
                    {
                        var userEmail = User.FindFirstValue(ClaimTypes.Email);
                        if (userEmail != null)
                        {
                            Programari = await _context.Programare
                           .Include(p => p.Biserica)
                           .Include(p => p.Enorias)
                           .Include(p => p.Preot)
                           .Include(p => p.Serviciu)
                           .Where(p => p.Enorias.Email == userEmail)
                           .ToListAsync();
                        }
                        else
                        {
                            Programari = new List<Programare>();
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading Programari: {ex.Message}");
                    throw;
                }
            }
        }

        public IActionResult OnPostCreateCheckoutSession(string amount)
        {
            var currency = "ron";

            var successUrl = "https://localhost:7044/";
            var cancelUrl = "https://localhost:7044/";

            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            if (decimal.TryParse(amount, out decimal amountValue))
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount = (long)(amountValue * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Product Name",
                            Description = "Product Description"
                        }
                    },
                    Quantity = 1
                }
            },
                    Mode = "payment",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl,
                };

                var service = new SessionService();
                var session = service.Create(options);
                SessionId = session.Id;

                return Redirect(session.Url);
            }
            else
            {
                Console.WriteLine($"Valore invalida: {amount}");
                return RedirectToPage("/Error");
            }
        }

    }
}
