
using Microsoft.EntityFrameworkCore;
using ProductAdminApi.Context;
using ProductAdminApi.Model;

namespace ProductAdminApi_Test
{
    public class ProductDbFixture
    {
        internal ProductDbContext _productDbContext;
        public ProductDbFixture()
        {
            var productDbContextOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase("ProductDb").Options;
            _productDbContext = new ProductDbContext(productDbContextOptions);
            _productDbContext.Add(new Product() { Id = 1, ProductName = "Iphone 14", Price = 80000, ImagePath = "iphone11.jpeg", Category = "mobile" });
            _productDbContext.Add(new Product() { Id = 2, ProductName = "SonyBravia", ImagePath = "string", Price = 40000, Category = "Tv" });
            _productDbContext.Add(new Product() { Id = 3, ProductName = "Thinkpad", ImagePath = "string", Price = 70000, Category = "Laptop" });
            _productDbContext.SaveChanges();
        }
    }
}