using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Data.Contexts;
using Demo.DataAccesse.Repositories.Interfaces;

namespace Demo.DataAccesse.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext DbContext) : GenericRepository<Employee>(DbContext) , IEmployeeRepository
    {

    }
}
