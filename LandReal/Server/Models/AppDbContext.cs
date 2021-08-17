using LandReal.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandReal.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //seed Department Table
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 1, DepartmentName = "IT" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 2, DepartmentName = "HR" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 3, DepartmentName = "PayRole" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 4, DepartmentName = "Admin" });
            //seed Employee Table
            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeId = 1, FirstName = "John", LastName = "Hastings", Email = "h@h.com", DateOfBirth = new DateTime(1980, 10, 5), Gender = Gender.Male, DepartmentId = 1, PhotoPath = "images/bab.jpg", Amount = 23.45, IsActive = true });
            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeId = 2, FirstName = "Abe", LastName = "Olufunmi", Email = "a@a.com", DateOfBirth = new DateTime(1988, 10, 5), Gender = Gender.Female, DepartmentId = 2, PhotoPath = "images/oab.jpg", Amount = 93.45, IsActive = false });
            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeId = 3, FirstName = "Ola", LastName = "lofet", Email = "o@h.com", DateOfBirth = new DateTime(1989, 10, 5), Gender = Gender.Male, DepartmentId = 3, PhotoPath = "images/yab.jpg", Amount = 83.45, IsActive = true });
            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeId = 4, FirstName = "Laide", LastName = "kings", Email = "p@h.com", DateOfBirth = new DateTime(1990, 10, 5), Gender = Gender.Male, DepartmentId = 4, PhotoPath = "images/bab.jpg", Amount = 66.45, IsActive = false });




        }
    }
}
