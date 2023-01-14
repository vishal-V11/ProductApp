using ProductAdminApi.Model;

namespace ProductAdminApi.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        bool AddProduct(Product product);
        Task<bool> Delete(int id);
        Task<bool> Edit(int id, Product product);
    }
}