using BookingAPI.Models;
using BookingAPI.Repository;
using BookingAPI.Services;
using Microsoft.Extensions.Configuration;

namespace BookingApiTest
{
    public class BookingApiServiceTest:IClassFixture<BookingDbFixture>
    {
        readonly IBookingRepository _bookingRepository;
        readonly IBookingService _bookingService;
        readonly IConfiguration configuration;

        public BookingApiServiceTest(BookingDbFixture bookingDbFixture)
        {
            _bookingRepository = new BookingRepository(bookingDbFixture._bookingDbContext);
            _bookingService = new BookingService(_bookingRepository, configuration);
        }

        

        [Fact]
        public void AddProductToCartShouldReturnTrue()
        {
            var expected = true;
            ProductCart item = new ProductCart() { CartId = 2, ProductName = "Lenovo Thinkbook", Category = "Laptop", Price = 40000, ImagePath = "jhskjakh", Name = "test1", Quantity = 1};
            var actual = _bookingService.AddItemsToCart(item);
            Assert.Equal(expected, actual);
        }

        

        [Fact]
        public void DeleteItemFromCartShouldReturnTrue()
        {
            var expected = true;
            var id = 3;
            var actual = _bookingService.RemoveItem(id)
;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ViewCartItemsReturnsCountOfItems()
        {
            var expected = 1;
            var username = "vishal";
            var actual = _bookingService.ViewCartItems(username);
            Assert.Equal(expected, actual.Count());
        }

        [Fact]
        public void GenerateBill_ReturnsIntIfUserIsValid()
        {
            var expected = 180000;
            var username = "vishal";
            var actual = _bookingService.GenerateBill(username);
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void PayBill_ShouldReturnTrue()
        {
            var expected = true;
            var username = "tejas";
            var actual = _bookingService.PayBill(username);
            Assert.Equal(expected, actual);
        }
    }
}