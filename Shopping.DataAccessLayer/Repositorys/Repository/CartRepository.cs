using Shopping.DataAccessLayer.Data;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.Entites.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.Repository
{
    public class CartRepository : Repository<ShoppingCart>, ICartRepository
    {
        private ApplicationDBContext applicationDBContext;

     

        public CartRepository(ApplicationDBContext _applicationDBContext) : base(_applicationDBContext)
        {
            applicationDBContext = _applicationDBContext;
        }

        public int DecreaseNum(ShoppingCart shoppingcart, int num)
        {

            shoppingcart.Count -= num;
            return shoppingcart.Count ;
        }

        // I Want To Ask If OK Save Database Edited Without Updated
        public int  IncreaseNum(ShoppingCart shoppingcart, int num)
        {
            shoppingcart.Count += num;
            return shoppingcart.Count;
        }

        public void Update(ShoppingCart shoppingcart)
        {
            //GetIDFromcategory
            var Oldshoppingcart = applicationDBContext.shoppingCarts.FirstOrDefault(x => x.Id == shoppingcart.Id);
            Oldshoppingcart.ProID = shoppingcart.ProID;
            Oldshoppingcart.UserData = shoppingcart.UserData;
            Oldshoppingcart.UserID = shoppingcart.UserID;
            Oldshoppingcart.Count += shoppingcart.Count;



            applicationDBContext.shoppingCarts.Update(Oldshoppingcart);
        }
    }
}
