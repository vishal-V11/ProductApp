

using BookingAPI.Context;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApiTest
{
    public class BookingDbFixture
    {
        internal BookingDbContext _bookingDbContext;
        public BookingDbFixture()
        {
            var bookingDbContextOptions = new DbContextOptionsBuilder<BookingDbContext>().UseInMemoryDatabase("ProductDb").Options;
            _bookingDbContext = new BookingDbContext(bookingDbContextOptions);
            _bookingDbContext.Add(new ProductCart() { CartId = 1,ProductName="Iphone14",Category="mobile",Price=90000, ImagePath ="string",Name="vishal",Quantity=2,Total=180000});
            _bookingDbContext.Add(new ProductCart() { CartId = 3, ProductName = "Sony", Category = "TV", Price = 50000, ImagePath = "string", Name = "aayush", Quantity = 1, Total = 50000 });
            _bookingDbContext.Add(new ProductCart() { CartId = 4, ProductName = "Samsung", Category = "AC", Price = 45000, ImagePath = "string", Name = "tejas", Quantity = 1, Total = 45000 });
            _bookingDbContext.SaveChanges();
        }
    }
}
