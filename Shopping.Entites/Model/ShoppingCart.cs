using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Entites.Model
{

    public class ShoppingCart
    {
        public int Id { get; set; }

        [ForeignKey("ProID")]
        [ValidateNever]
        public Product Product { get; set; }
        public int ProID { get; set; }

        public int Count { get; set; }


        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public ExstraUserData UserData { get; set; }

    }
}
