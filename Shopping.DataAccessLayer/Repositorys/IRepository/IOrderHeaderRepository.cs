using Shopping.Entites.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Repositorys.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader> 
    {
        //Category category
        void Update(OrderHeader orderHeader);

        //Update Status(Order And Payment)

        void UpdateStatusOrder(int id,string orderStatus,string paymentStatus);
    }
}
