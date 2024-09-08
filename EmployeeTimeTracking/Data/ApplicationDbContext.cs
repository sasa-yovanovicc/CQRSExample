using EmployeeTimeTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EmployeeTimeTracking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext() { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkInterval> WorkIntervals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.WorkIntervals)
                .WithOne(wi => wi.Employee)
                .HasForeignKey(wi => wi.EmployeeId);
            
            modelBuilder.Entity<Employee>()
                 .Property(e => e.TotalHours)
                 .HasColumnType("decimal(7,2)");

            modelBuilder.Entity<WorkInterval>()
                .HasOne(w => w.Employee)
                .WithMany(e => e.WorkIntervals)
                .HasForeignKey(w => w.EmployeeId);

            modelBuilder.Entity<WorkInterval>()
                .Property(wi => wi.Start)
                .HasColumnType("datetime") 
                .IsRequired(false);

            modelBuilder.Entity<WorkInterval>()
                .Property(wi => wi.End)
                .HasColumnType("datetime")
                .IsRequired(false);
        }
    }

}