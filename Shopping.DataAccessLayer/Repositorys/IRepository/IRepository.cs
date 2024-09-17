using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.IRepository
{
    public interface IRepository<T>where T : class
    {
        //IEnumerable<T>
        IEnumerable<T> Get(Expression<Func<T,bool>> expression=null,string include=null);

        T GetFirst(Expression<Func<T, bool>> expression = null, string include = null);

        void Add(T entity);

        void Delete(T entity);

        //IEnumerable<T>
        void DeleteRange(IEnumerable<T> entity);

    }
}
