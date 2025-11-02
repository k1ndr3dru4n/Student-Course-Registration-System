using Microsoft.EntityFrameworkCore;
using SMS.Models;

namespace SMS
{
    public class CimContext : DbContext
    {
        public CimContext(DbContextOptions<CimContext> options) : base(options) 
        { 
            // 禁用延迟加载以避免循环引用
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<Student> SMS_Students { get; set; }
        public DbSet<Teacher> SMS_Teachers { get; set; }
        public DbSet<User> SMS_Users { get; set; }
        public DbSet<Course> SMS_Courses { get; set; }
        public DbSet<Enrollment> SMS_Enrollments { get; set; }
        public DbSet<TeacherApplication> SMS_TeacherApplications { get; set; } // 新增

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置Enrollment的外键关系
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentSID)
                .HasPrincipalKey(s => s.SID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseCID)
                .HasPrincipalKey(c => c.CID);

            // 配置Course的外键关系到Teacher
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(t => t.Id)
                .IsRequired(false);

            // 配置User与学生/教师的关联关系
            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)
                .WithMany()
                .HasForeignKey(u => u.StudentId)
                .HasPrincipalKey(s => s.Id)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Teacher)
                .WithMany()
                .HasForeignKey(u => u.TeacherId)
                .HasPrincipalKey(t => t.Id)
                .IsRequired(false);

            // 配置TeacherApplication的外键关系 - 新增
            modelBuilder.Entity<TeacherApplication>()
                .HasOne(ta => ta.Teacher)
                .WithMany()
                .HasForeignKey(ta => ta.TeacherId)
                .HasPrincipalKey(t => t.Id)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Student || e.Entity is Teacher || e.Entity is User || e.Entity is Course || e.Entity is Enrollment || e.Entity is TeacherApplication); // 新增 TeacherApplication
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    // 使用北京时间（UTC+8）
                    var beijingTime = DateTime.UtcNow.AddHours(8);
                    entry.Property("CreateDate").CurrentValue = beijingTime;
                    entry.Property("UpdateDate").CurrentValue = beijingTime;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // 使用北京时间（UTC+8）
                    var beijingTime = DateTime.UtcNow.AddHours(8);
                    entry.Property("UpdateDate").CurrentValue = beijingTime;
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Student || e.Entity is Teacher || e.Entity is User || e.Entity is Course || e.Entity is Enrollment || e.Entity is TeacherApplication); // 新增 TeacherApplication
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    // 使用北京时间（UTC+8）
                    var beijingTime = DateTime.UtcNow.AddHours(8);
                    entry.Property("CreateDate").CurrentValue = beijingTime;
                    entry.Property("UpdateDate").CurrentValue = beijingTime;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // 使用北京时间（UTC+8）
                    var beijingTime = DateTime.UtcNow.AddHours(8);
                    entry.Property("UpdateDate").CurrentValue = beijingTime;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}