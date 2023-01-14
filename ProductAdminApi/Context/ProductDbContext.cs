
using Microsoft.EntityFrameworkCore;
using ProductAdminApi.Model;

namespace ProductAdminApi.Context
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> context):base(context)
        {

        }
       
        public DbSet<Product> ProductList { get; set; }
    }
}
