using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Entites.Model
{
    public class OrderDetails
    {
        public int ID { get; set; }

        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        [ValidateNever]
        public OrderHeader Header { get; set; }

        public int ProID { get; set; }
        [ValidateNever]
        [ForeignKey("ProID")]

        public Product Product { get; set; }

        public int Count {  get; set; }

        public decimal Price {  get; set; }
    }
}
