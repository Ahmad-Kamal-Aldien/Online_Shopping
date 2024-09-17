using Shopping.DataAccessLayer.Data;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //Pass connection DB and new initailaze  
        public ApplicationDBContext applicationDBContext;
        public ICategoryRepository category { get; set; }
        public ICartRepository cart { get; set; }
        public IProductRepository product { get; set; }

        public IOrderHeaderRepository orderHeader { get; set; }

        public IOrderDetailsRepository orderDetails { get; set; }

        public IUserRepository user { get; set; }


        public UnitOfWork(ApplicationDBContext _applicationDBContext)
        {
            applicationDBContext= _applicationDBContext;
            category = new CategoryRepository(_applicationDBContext);
            product = new ProductRepository(_applicationDBContext);
            cart= new CartRepository(_applicationDBContext);
            orderHeader = new OrderHeaderRepository(_applicationDBContext);
            orderDetails = new OrderDetailsRepository(_applicationDBContext);
            user = new UserRepository(_applicationDBContext);


        }


        public int complete()
        {
            return applicationDBContext.SaveChanges();
        }
    }
}
