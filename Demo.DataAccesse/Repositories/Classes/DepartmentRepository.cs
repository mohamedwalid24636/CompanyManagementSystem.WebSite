using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Data.Contexts;
using Demo.DataAccesse.Models.DepartmentModel;
using Demo.DataAccesse.Repositories.Interfaces;

namespace Demo.DataAccesse.Repositories.Classes
{   // خد بالك ان (السي ال ار ) شغال عال ال انجيكشن اللي هنا DbContext
    //بعت لل كونستراكتور القديم ال DbContext
    public class DepartmentRepository(ApplicationDbContext DbContext) : GenericRepository<Department>(DbContext) , IDepartmentRepository 

    {
        private readonly ApplicationDbContext _DbContext = DbContext;



        #region CRUD Operations

        //Get All 
        //public IEnumerable<Department> GetAll(bool WithTracking = false)
        //{
        //    if (WithTracking)
        //        return _DbContext.Departments;
        //    else
        //        return _DbContext.Departments.AsNoTracking().ToList();
        //}


        ////Get by Id
        //public Department? GetById(int Id) => _DbContext.Departments.Find(Id);


        ////Update
        //public int Update(Department department)
        //{
        //    _DbContext.Departments.Update(department);
        //    return _DbContext.SaveChanges();
        //}


        ////Insert  
        //public int insert(Department department)
        //{
        //    _DbContext.Departments.Add(department);
        //    return _DbContext.SaveChanges();
        //}


        ////Delete
        //public int Remove(Department department)
        //{
        //    _DbContext.Departments.Remove(department); 
        //    return _DbContext.SaveChanges();
        //}


        #endregion



    }
}
