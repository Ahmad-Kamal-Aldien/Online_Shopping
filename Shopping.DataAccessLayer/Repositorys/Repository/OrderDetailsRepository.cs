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
   public  class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private ApplicationDBContext applicationDBContext;

         //base(_applicationDBContext)

        public OrderDetailsRepository(ApplicationDBContext _applicationDBContext):base(_applicationDBContext)
        {
            applicationDBContext=_applicationDBContext;
        }

        public void Update(OrderDetails orderDetails)
        {
            //GetIDFromOrderHeader
            var pro = applicationDBContext.orderDetails.FirstOrDefault(x=>x.ID== orderDetails.ID);
            pro.Price= orderDetails.Price;
            pro.ProID = orderDetails.ProID;



            applicationDBContext.orderDetails.Update(pro);

        }
    }
}
