using Coursework.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.DataAccess
{
    public class PechDbContext : DbContext
    {
        public PechDbContext(DbContextOptions<PechDbContext> options)
            : base(options)
        {
        }
        public DbSet<PechEntity> Pech { get; set; }
    }
}
