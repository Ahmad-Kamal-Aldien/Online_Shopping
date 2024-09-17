using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Entites.Model
{
    public class ExstraUserData:IdentityUser
    {
        public string fullName { get; set; }

        public string Address { get; set; }

        public string City {  get; set; }


    }
}
