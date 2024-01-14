using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proiect.Data;
using Microsoft.AspNetCore.Identity;
using Proiect.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
           policy.RequireRole("Admin"));
        });


        // Add services to the container.
        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/Servicii");
            options.Conventions.AllowAnonymousToPage("/Servicii/Index");
            options.Conventions.AllowAnonymousToPage("/Servicii/Details");
            options.Conventions.AuthorizeFolder("/Enoriasi", "AdminPolicy");

        });
        builder.Services.AddDbContext<ProiectContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ProiectContext") ?? throw new InvalidOperationException("Connection string 'ProiectContext' not found.")));


        builder.Services.AddDbContext<LibraryIdentityContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("ProiectContext") ?? throw new InvalidOperationException("Connectionstring 'Nume_Pren_Lab2Context' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()

        .AddEntityFrameworkStores<LibraryIdentityContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication(); ;

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}