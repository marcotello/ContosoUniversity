using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<Person> People { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
            modelBuilder.Entity<Person>().ToTable("Person");

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });

            //By convention, the Entity Framework enables cascade delete for non-nullable foreign keys and for many-to-many relationships. 
            //This can result in circular cascade delete rules, which will cause an exception when you try to add a migration. For example, 
            //if you didn't define the Department.InstructorID property as nullable, EF would configure a cascade delete rule to delete the 
            //instructor when you delete the department, which is not what you want to have happen. If your business rules required the 
            //InstructorID property to be non-nullable, you would have to use the following fluent API statement to disable cascade delete 
            //on the relationship.
            //
            //modelBuilder.Entity<Department>()
            //    .HasOne(d => d.Administrator)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict)
        }
    }
}