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
   public  class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDBContext applicationDBContext;

         //base(_applicationDBContext)

        public OrderHeaderRepository(ApplicationDBContext _applicationDBContext):base(_applicationDBContext)
        {
            applicationDBContext=_applicationDBContext;
        }

        public void Update(OrderHeader orderHeader)
        {
            //GetIDFromOrderHeader
            var pro = applicationDBContext.orderHeaders.FirstOrDefault(x=>x.ID== orderHeader.ID);
            pro.Name= orderHeader.Name;


          

            applicationDBContext.orderHeaders.Update(pro);

        }

        public void UpdateStatusOrder(int id, string orderStatus, string paymentStatus)
        {
            var order = applicationDBContext.orderHeaders.FirstOrDefault(x=>x.ID==id);
            if (order != null)
            {
                order.orderStatus = orderStatus;
                order.PaymentDate = DateTime.Now;
                order.PaymentStatus= paymentStatus;
            }
        }
    }
}
