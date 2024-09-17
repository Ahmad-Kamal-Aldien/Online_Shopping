using Shopping.DataAccessLayer.Data;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.Entites.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.Repository
{
    //,CategoryIRepository
   public  class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        private ApplicationDBContext applicationDBContext;

         //base(_applicationDBContext)

        public CategoryRepository(ApplicationDBContext _applicationDBContext):base(_applicationDBContext)
        {
            applicationDBContext=_applicationDBContext;
        }

        public void Update(Category category)
        {
            //GetIDFromcategory
            var cat = applicationDBContext.Category.FirstOrDefault(x=>x.Id== category.Id);
            cat.Name=category.Name;
            cat.Description=category.Description;
            cat.CreatedDate=DateTime.Now;
            applicationDBContext.Category.Update(cat);

        }
    }
}
