using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Entites.Model.ViewModels
{
    public class CartViewModel
    {
        [ValidateNever]
        public IEnumerable<ShoppingCart> carts { get; set; }
        public decimal total {  get; set; }

        public OrderHeader orderheader { get; set; }
     

     
       
    }
}
