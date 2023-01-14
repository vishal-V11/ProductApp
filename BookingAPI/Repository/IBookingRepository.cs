using BookingAPI.Models;

namespace BookingAPI.Repository
{
    public interface IBookingRepository
    {
        bool AddItems(ProductCart addItems);
        ProductCart CheckIfItemPresentForParticularUser(string productname,string username);
       
        List<ProductCart> GetCart(string userName);
        bool GetItemByCartId(int cartId);
        //List<Menu> GetMenu();
        bool PayBill(string userName);
        bool RemoveItem(int cartId);
        bool UpdateQuantity(ProductCart update);
        int GetTotalAmount(string userName);
    }
}
