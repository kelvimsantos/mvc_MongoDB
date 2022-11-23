using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mvc_MongoDB.Models;

namespace mvc_MongoDB.Data
{
    public class mvc_MongoDBContext : DbContext
    {
        public mvc_MongoDBContext (DbContextOptions<mvc_MongoDBContext> options)
            : base(options)
        {
        }

        public DbSet<mvc_MongoDB.Models.Usuario> Usuario { get; set; } = default!;
    }
}
