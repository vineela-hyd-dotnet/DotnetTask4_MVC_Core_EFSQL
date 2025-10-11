
using DotnetTask4_MVC_Core_EFQL.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetTask4_MVC_Core_EFSQL.Models

{

    public class AppDbContext : DbContext

    {

        public AppDbContext(DbContextOptions<AppDbContext> options)

            : base(options)

        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<Attendance>()

                .HasIndex(a => new { a.EmployeeId, a.Date })

                .IsUnique();

         

            modelBuilder.Entity<Attendance>()

                .HasOne(a => a.Employee)

                .WithMany() 

                .HasForeignKey(a => a.EmployeeId)

                .OnDelete(DeleteBehavior.Cascade);

        }

    }

}

