using Microsoft.EntityFrameworkCore;
using SMS.Models;

namespace SMS
{
    public class CimContext : DbContext
    {
        public CimContext(DbContextOptions<CimContext> options) : base(options) { }
        public DbSet<Student> SMS_Students { get; set; }
        public DbSet<Teacher> SMS_Teachers { get; set; }    }
}