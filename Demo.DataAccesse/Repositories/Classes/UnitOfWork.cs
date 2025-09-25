using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Data.Contexts;
using Demo.DataAccesse.Repositories.Interfaces;

namespace Demo.DataAccesse.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork  /*IDisposable*/
    {
        public readonly Lazy<IDepartmentRepository> _DepartmentRepository;
        public readonly Lazy<IEmployeeRepository> _EmployeeRepository;
        private readonly ApplicationDbContext dbContext;

        public UnitOfWork(/*IDepartmentRepository DepartmentRepository , IEmployeeRepository EmployeeRepository ,*/ ApplicationDbContext dbContext  ) 
        { 
            this.dbContext = dbContext;
            _DepartmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _EmployeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
        }
        public IEmployeeRepository EmployeeRepository => _EmployeeRepository.Value;   //.value  عشان ال lazy مش بيعمل instantiate الا لما تيجي تستخدمه

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository.Value;

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public int SaveChanges()=> dbContext.SaveChanges();
        
    }
}
