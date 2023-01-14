namespace ProductAdminApi.Exceptions
{
    public class ProductDoesNotExistException:Exception
    {
        public ProductDoesNotExistException(string msg):base(msg)
        {

        }

        public ProductDoesNotExistException()
        {

        }
    }
}
