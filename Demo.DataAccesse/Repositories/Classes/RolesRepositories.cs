using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Data.Contexts;

namespace Demo.DataAccesse.Repositories.Classes
{
    internal class RolesRepositories(ApplicationDbContext _dbContext )
    {
        public IEnumerable<IdentityRole> GetAll(bool WithTracking )
        {
            if (WithTracking)
                return _dbContext.Roles.ToList();
            else
                return _dbContext.Roles.AsNoTracking().ToList();
        }

        public IdentityRole? GetById(string Id)
        {
            return _dbContext.Roles.Find(Id);
        }

        //Create 

        public int Create(IdentityRole Role)
        {
           _dbContext.Roles.Add(Role);
            return _dbContext.SaveChanges();

        }


        //Update

        public int Update(IdentityRole Role)
        {
            _dbContext.Roles.Update(Role);
            return _dbContext.SaveChanges();
        }


        //Delete

        public bool Delete(IdentityRole Role)
        {
            _dbContext.Roles.Remove(Role);
           
            return _dbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
