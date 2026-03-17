using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using agenda.Models;

namespace agenda.Data
{
    public class BDContext : DbContext
    {
        public BDContext (DbContextOptions<BDContext> options)
            : base(options)
        {
        }

        public DbSet<agenda.Models.Agenda> Agenda { get; set; } = default!;
    }
}
