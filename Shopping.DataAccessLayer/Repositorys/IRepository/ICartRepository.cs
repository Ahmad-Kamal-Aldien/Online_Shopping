using Shopping.Entites.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.IRepository
{
    public interface ICartRepository : IRepository<ShoppingCart>
    {
        //Category category
        void Update(ShoppingCart shoppingcart);

        int IncreaseNum(ShoppingCart shoppingcart,int num);
        int DecreaseNum(ShoppingCart shoppingcart,int num);

    }




   
}
