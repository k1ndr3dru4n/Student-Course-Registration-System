using Microsoft.EntityFrameworkCore;
using SMS.Models;

namespace SMS
{
    public class CimContext : DbContext
    {
        public CimContext(DbContextOptions<CimContext> options) : base(options) { }
        public DbSet<Student> SMS_Students { get; set; }
        public DbSet<Teacher> SMS_Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> SMS_Courses { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Student || e.Entity is Teacher || e.Entity is User || e.Entity is Course);
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("UpdateDate").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdateDate").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Student || e.Entity is Teacher || e.Entity is User || e.Entity is Course);
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("UpdateDate").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdateDate").CurrentValue = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}