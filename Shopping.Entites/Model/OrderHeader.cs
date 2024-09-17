using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Entites.Model
{
    public class OrderHeader
    {

        public int ID { get; set; }
      
        public string NameUserID { get; set; }
        [ForeignKey("NameUserID")]
        [ValidateNever]
        public ExstraUserData UserData { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }

        public decimal total {  get; set; }

        public string? orderStatus {  get; set; }

        public string? Carrier { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? PaymentStatus { get; set;}

        //User Info Exstra

        public string Name { get; set; }
        public string Address { get; set; }

        public string City { get; set; }
        public string PhoneNumber { get; set; }


        //Stripe
        public string? SessionID { get;set; }

        public string? PaymentIntentId { get; set; }


    }
}
