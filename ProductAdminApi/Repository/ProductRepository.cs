using Microsoft.EntityFrameworkCore;
using ProductAdminApi.Context;
using ProductAdminApi.Model;

namespace ProductAdminApi.Repository
{
    public class ProductRepository:IProductRepository
    {
        readonly ProductDbContext _productDbContext;
        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }


        public int AddProduct(Product product)
        {
            _productDbContext.ProductList.Add(product);
            return _productDbContext.SaveChanges();
        }

        public Task<int> Delete(Product producToDelete)
        {
            _productDbContext.Remove(producToDelete);
            return _productDbContext.SaveChangesAsync();
        }

        public Task<int> Edit(Product product,Product productToUpdate)
        {
            //_productDbContext.Entry(product).State = EntityState.Modified;
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Price = product.Price;
            productToUpdate.Category = product.Category;
            productToUpdate.ImagePath = product.ImagePath;

            return _productDbContext.SaveChangesAsync();
        }

        public Task<List<Product>> GetAllProducts()
        {
            return _productDbContext.ProductList.ToListAsync();
        }

        public Task<Product> GetProductById(int id)
        {
            return _productDbContext.ProductList.Where(product => product.Id == id).FirstOrDefaultAsync();
        }

        public Product GetProductByName(string productName)
        {
            return _productDbContext.ProductList.Where(product => product.ProductName == productName).FirstOrDefault();
        }

    }
}
