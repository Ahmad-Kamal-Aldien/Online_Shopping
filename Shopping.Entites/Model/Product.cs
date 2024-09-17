using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Entites.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        public string Image {  get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        [ValidateNever]

        public Category Category { get; set; }
        [DisplayName("Choose Category")]
        public int CategoryId {  get; set; }

    }
}
