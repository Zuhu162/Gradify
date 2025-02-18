using Microsoft.EntityFrameworkCore;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
    }
}
