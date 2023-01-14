using BookingAPI.Models;

using Newtonsoft.Json.Linq;

namespace BookingAPI.Services
{
    public interface IBookingService
    {
        bool AddItemsToCart(ProductCart addItems);
        //List<Menu> GetAllFoods();
        
        bool PayBill(string userName);
        bool RemoveItem(int cartId);
        List<ProductCart> ViewCartItems(string UserName);
        int GenerateBill(string userName);
        string InVoiceNo();

        void SendEmail(string userEmail, EmailDetails request);


    }
}
