using Demo.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MVCAppDBContext: IdentityDbContext<ApplicationUser>
    {
        public MVCAppDBContext(DbContextOptions<MVCAppDBContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        //optionsBuilder.UseSqlServer("server = .;database = MVCAppDB; user=Admin;password=12345;Trusted_Connection=True;Encrypt=False");

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
}
}
