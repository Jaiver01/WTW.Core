using Microsoft.EntityFrameworkCore;

namespace WTW.Core.Models {
    public class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Ignore(x => x.FullName);
            modelBuilder.Entity<Person>().Ignore(x => x.Identification);
        }
    }
}
