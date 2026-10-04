using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Data.Contexts;
using Demo.DataAccesse.Models.DepartmentModel;
using Demo.DataAccesse.Models.Shared;
using Demo.DataAccesse.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccesse.Repositories.Classes
{
    public class GenericRepository <TEntity>(ApplicationDbContext _DbContext) :IGenericRepository<TEntity> where TEntity : BaseEntity 
    {
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _DbContext.Set<TEntity>().Where(E=>E.IsDeleted !=true).ToList();      // خد بالك مسكت جدول الااقسام بال set <TEntity>
            else
                return _DbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();   //Soft Delete
        }



        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector)
        {
            return _DbContext.Set<TEntity>().Where(E => E.IsDeleted != true)
                                            .Select(Selector)
                                            .ToList();
        }


        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
            {
                return _DbContext.Set<TEntity>()
                    .Where(E => E.IsDeleted != true)
                    .Where(Predicate)
                    .ToList();
            }





        //Get by Id
        public TEntity? GetById(int Id) => _DbContext.Set<TEntity>().Find(Id);


        //Update
        public void Update(TEntity entity)
        {
            _DbContext.Set<TEntity>().Update(entity);
            //return _DbContext.SaveChanges();  UNIt Of Work
        }


        //Insert  
        public void insert(TEntity entity)
        {
            _DbContext.Set<TEntity>().Add(entity);
            //return _DbContext.SaveChanges();           UNIt Of Work
        }


        //Delete
        public void Remove(TEntity entity)
        {
            _DbContext.Set<TEntity>().Remove(entity);
            //return _DbContext.SaveChanges();           UNIt Of Work
        }

    
    }
}
