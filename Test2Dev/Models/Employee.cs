using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test2Dev.Enums;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2Dev.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(13)]
        [Index(IsUnique = true)]
        public string RFC { get; set; }
        public DateTime BornDate { get; set; }
        [Required]
        public EmployeeStatus Status { get; set; }
    }

    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(){ }
        public DbSet<Employee> Employees { get; set; }
        
    }
}