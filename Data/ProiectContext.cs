using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Data
{
    public class ProiectContext : DbContext
    {
        public ProiectContext (DbContextOptions<ProiectContext> options)
            : base(options)
        {
        }

        public DbSet<Proiect.Models.Biserica> Biserica { get; set; } = default!;

        public DbSet<Proiect.Models.Serviciu>? Serviciu { get; set; }

        public DbSet<Proiect.Models.Preot>? Preot { get; set; }

        public DbSet<Proiect.Models.Enorias>? Enorias { get; set; }

        public DbSet<Proiect.Models.Programare>? Programare { get; set; }
    }
}
