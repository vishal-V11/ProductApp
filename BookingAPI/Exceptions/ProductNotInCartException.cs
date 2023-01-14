namespace BookingAPI.Exceptions
{
    public class ProductNotInCartException:ApplicationException
    {
        public ProductNotInCartException(string msg):base(msg)
        {

        }

        public ProductNotInCartException():base()
        {

        }
    }
}
