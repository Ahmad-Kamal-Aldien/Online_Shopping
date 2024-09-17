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
    public class UserRepository : Repository<ExstraUserData>, IUserRepository
    {
        private ApplicationDBContext applicationDBContext;

     

        public UserRepository(ApplicationDBContext _applicationDBContext) : base(_applicationDBContext)
        {
            applicationDBContext = _applicationDBContext;
        }

       

        public void Update(ShoppingCart shoppingcart)
        {
            //GetIDFromcategory
            var Oldshoppingcart = applicationDBContext.shoppingCarts.FirstOrDefault(x => x.Id == shoppingcart.Id);
            Oldshoppingcart.ProID = shoppingcart.ProID;
            Oldshoppingcart.UserData = shoppingcart.UserData;
            Oldshoppingcart.UserID = shoppingcart.UserID;
            Oldshoppingcart.Count = shoppingcart.Count;



            applicationDBContext.shoppingCarts.Update(Oldshoppingcart);
        }
    }
}
