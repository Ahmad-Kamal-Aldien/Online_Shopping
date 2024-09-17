using Shopping.Entites.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository category { get; }
        IProductRepository product { get; }

        ICartRepository cart { get; }
        IOrderHeaderRepository orderHeader { get; }

        IOrderDetailsRepository orderDetails { get; }

        IUserRepository user { get; }
        int complete();
    }
}
