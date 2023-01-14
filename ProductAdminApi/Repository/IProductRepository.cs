using ProductAdminApi.Model;

namespace ProductAdminApi.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Product GetProductByName(string productName);
        int AddProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<int> Delete(Product product);
        Task<int> Edit(Product product, Product productToUpdate);
    }
}
