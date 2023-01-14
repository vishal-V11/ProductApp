namespace ProductAdminApi.Exceptions
{
    public class ProductAlreadyExistException:ApplicationException
    {
        public ProductAlreadyExistException(string msg):base(msg)
        {

        }

        public ProductAlreadyExistException()
        {

        }
    }
}
