using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stok_Kontrol_API.Entities.Entities;

namespace Stok_Kontrol_API.Repositories
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Stok_Kontrol_API.Entities.Entities.Supplier> Supplier { get; set; } = default!;
    }
}
