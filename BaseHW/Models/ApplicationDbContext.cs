using BaseHW.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseHW.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Sikuyear> Sikuyear { get; set; }

        public DbSet<SikuSikuyear> SikuSikuyear { get; set; }

        public DbSet<Siku> Siku { get; set; }


        public DbSet<Hwyear> Hwyear { get; set; }

        public DbSet<HwHwyear> HwHwyear { get; set; }

        public DbSet<Hw> Hw { get; set; }

        public DbSet<Setting> Settings { get; set; }

    }
}
