using Shopping.Entites.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.IRepository
{
    public interface ICategoryRepository: IRepository<Category> 
    {
        //Category category
        void Update(Category category);
    }
}
