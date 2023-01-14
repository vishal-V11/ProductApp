using BookingAPI.Context;
using BookingAPI.Models;

namespace BookingAPI.Repository
{
    public class BookingRepository : IBookingRepository
    {
        static int ?GrandTotal;
        readonly BookingDbContext _bookingDbContext;
        public BookingRepository(BookingDbContext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
        }

        /// <summary>
        /// Add The item to the Cart
        /// </summary>
        /// <param name="addItems"></param>
        public bool AddItems(ProductCart addItems)
        {
            _bookingDbContext.addToCarts.Add(addItems);
            //_bookingDbContext.SaveChanges();
            addItems.Total = addItems.Quantity * addItems.Price;
            //GrandTotal += addItems.Total;
            return _bookingDbContext.SaveChanges()==1?true:false;
            
        }

        /// <summary>
        /// Checking if the Item is already Present in the Cart for a Particular user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ProductCart CheckIfItemPresentForParticularUser(string productName,string userName)
        {
            return _bookingDbContext.addToCarts.Where(u => u.ProductName == productName&& u.Name == userName).FirstOrDefault();
        }

        /// <summary>
        /// Generate The bill
        /// </summary>
        /// <returns></returns>

        public int GetTotalAmount(string userName)
        {
            int TotalPrice = 0;
            List<ProductCart> cartList = _bookingDbContext.addToCarts.Where(c => c.Name == userName).ToList();
            if (cartList != null)
            {
                foreach (var item in cartList)
                {
                    TotalPrice += (int)item.Total;
                }
                return TotalPrice;
            }
            return 0;
        }

        /// <summary>
        /// Get The Cart of a Particular user
        /// </summary>
        /// <returns></returns>

        public List<ProductCart> GetCart(string userName)
        {
            return _bookingDbContext.addToCarts.Where(cart=>cart.Name==userName).ToList();
        }

        /// <summary>
        /// Check ifthe item is present to remove it
        /// </summary>
        /// <param name="removeItem"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        
        public bool GetItemByCartId(int cartId)
        {
            ProductCart present = _bookingDbContext.addToCarts.Where(u => u.CartId==cartId).FirstOrDefault();
            if(present != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Paying the bill and emptying tge cart
        /// </summary>
        public bool PayBill(string userName)
        {
            List<ProductCart> cartList = _bookingDbContext.addToCarts.Where(cart => cart.Name == userName).ToList();
            if(cartList != null)
            {
                foreach (var item in cartList)
                {
                    _bookingDbContext.addToCarts.Remove(item);
                }
            }
            int checkChanges = _bookingDbContext.SaveChanges();
            if (checkChanges >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// If Quantity is greater than 1 then derease it else remove the item
        /// </summary>
        /// <param name="removeItem"></param>
        public bool RemoveItem(int cartId)
        {
            ProductCart present = _bookingDbContext.addToCarts.Where(u =>u.CartId==cartId).FirstOrDefault();
            if(present.Quantity > 1)
            {
                present.Quantity -= 1;
                present.Total -= present.Price;
                return _bookingDbContext.SaveChanges()==1?true:false;
            }
            else
            {
                _bookingDbContext.Remove(present);
                return _bookingDbContext.SaveChanges()==1?true:false;
            }

        }

        /// <summary>
        /// If The Item is present Update the Quantity
        /// </summary>
        /// <param name="update"></param>
        public bool UpdateQuantity(ProductCart productItem)
        {
            ProductCart existingProductItem = _bookingDbContext.addToCarts.Where(u => u.Name == productItem.Name && u.ProductName== productItem.ProductName).FirstOrDefault();
            existingProductItem.Quantity += productItem.Quantity;
            existingProductItem.Total = 0;
            existingProductItem.Total = existingProductItem.Quantity*productItem.Price;  
            return _bookingDbContext.SaveChanges()==1?true:false;
            
        }
    }
}
