using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shopping.Entites.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DataAccessLayer.Data
{
    public class ApplicationDBContext:IdentityDbContext<IdentityUser>

    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options) :base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ExstraUserData> ExstraUserDatas { get; set; }

        public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<OrderHeader> orderHeaders { get; set; }

        public DbSet<OrderDetails> orderDetails { get; set; }


    }
}
