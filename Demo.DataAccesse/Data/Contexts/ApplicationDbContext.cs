using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Data.ModelsConfiguratoin;
using Demo.DataAccesse.Models.DepartmentModel;
using Demo.DataAccesse.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Demo.DataAccesse.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : /*DbContext(Options)*/ IdentityDbContext<ApplicationUser>(Options)  //===> identity tables  
    {


        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=DemoDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(modelBuilder);   //=>> apply configurations of hte identity package
        }
    }
}


// بعتمل ال dbset 
// عشان ممسكهاش بال setof