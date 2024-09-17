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
   public  class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDBContext applicationDBContext;

         //base(_applicationDBContext)

        public ProductRepository(ApplicationDBContext _applicationDBContext):base(_applicationDBContext)
        {
            applicationDBContext=_applicationDBContext;
        }

        public void Update(Product product)
        {
            //GetIDFromcategory
            var pro = applicationDBContext.Product.FirstOrDefault(x=>x.Id== product.Id);
            pro.Name= product.Name;
            pro.Description= product.Description;
            pro.CategoryId = product.CategoryId;
            pro.Price = product.Price;
            pro.Image = product.Image;
            applicationDBContext.Product.Update(pro);

        }
    }
}
