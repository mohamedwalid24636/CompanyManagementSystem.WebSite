using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Data.Contexts;
using Demo.DataAccesse.Models.IdentityModels;
using Demo.DataAccesse.Repositories.Interfaces;

namespace Demo.DataAccesse.Repositories.Classes
{
    public class UserRepositories(ApplicationDbContext _dbContext)
    {
        public IEnumerable<ApplicationUser> GetAll(bool WithTracking )
        {
            if (WithTracking)
                return _dbContext.Users.ToList();
            else
                return _dbContext.Users.AsNoTracking().ToList();
        }
    }

}
