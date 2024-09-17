using Microsoft.EntityFrameworkCore;
using Shopping.DataAccessLayer.Data;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //Context
        //DbSet

        private ApplicationDBContext applicationDBContext;
        private DbSet<T> dbSet;
        public Repository(ApplicationDBContext _applicationDBContext)
        {
            applicationDBContext= _applicationDBContext;
            dbSet= _applicationDBContext.Set<T>();
        }
        public void Add(T entity)
        {
            applicationDBContext.Add(entity);
        }

        public void Delete(T entity)    
        {
            applicationDBContext.Remove(entity);
        }

      

        public void DeleteRange(IEnumerable<T> entity)
        {
            applicationDBContext.RemoveRange(entity);
        }

      

        public IEnumerable<T> Get(Expression<Func<T, bool>> expression = null, string include = null)
        {
            IQueryable<T> query=dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (include != null)
            {
                foreach (var entity in include.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(entity);
                }
            }
            return query.ToList();
        }

        public T GetFirst(Expression<Func<T, bool>> expression = null, string include = null)
        {
            IQueryable<T> query= dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (include != null)
            {
                foreach (var entity in include.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(entity);
                }
            }
            //return query.Single();
            //return query.Single();
            return query.SingleOrDefault();

        }
    }
}
