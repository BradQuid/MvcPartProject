using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcPart.Models;

namespace MvcPart.Models
{
    public class MvcPartContext : DbContext
    {
        public MvcPartContext (DbContextOptions<MvcPartContext> options)
            : base(options)
        {
        }

        public DbSet<MvcPart.Models.Part> Part { get; set; }

        public DbSet<MvcPart.Models.Project> Project { get; set; }
    }
}
