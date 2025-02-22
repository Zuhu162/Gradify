using Microsoft.EntityFrameworkCore;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentAssignment>()
            .HasKey(sa => new { sa.UserId, sa.AssignmentId });

            modelBuilder.Entity<StudentAssignment>()
            .HasOne(sa => sa.User)
            .WithMany(u => u.StudentAssignments)
            .HasForeignKey(sa => sa.UserId) // Change StudentId → UserId
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentAssignment>()
                .HasOne(sa => sa.Assignment)
                .WithMany(a => a.StudentAssignments)
                .HasForeignKey(sa => sa.AssignmentId)
                .OnDelete(DeleteBehavior.NoAction);  // ❌ No Cascade Delete

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);  // ❌ No Cascade Delete

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Assignment)
                .WithMany()
                .HasForeignKey(s => s.AssignmentId)
                .OnDelete(DeleteBehavior.NoAction);  // ❌ No Cascade Delete
        }
      
    }
}
