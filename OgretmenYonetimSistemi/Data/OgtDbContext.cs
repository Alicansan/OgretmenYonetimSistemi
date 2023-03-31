using Microsoft.EntityFrameworkCore;
using OgretmenYonetimSistemi.Models.Domain;

namespace OgretmenYonetimSistemi.Data
{
    public class OgtDbContext : DbContext
    {
        public OgtDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ogretmen> Ogretmenler { get; set; }
    }
}
