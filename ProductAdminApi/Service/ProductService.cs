using ProductAdminApi.Exceptions;
using ProductAdminApi.Model;
using ProductAdminApi.Repository;

namespace ProductAdminApi.Service
{
    public class ProductService : IProductService
    {
        readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool AddProduct(Product product)
        {
            Product producExist = _productRepository.GetProductByName(product.ProductName);
            if(producExist == null)
            {
                int addProduct =  _productRepository.AddProduct(product);
                if(addProduct == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ProductAlreadyExistException($"{product.ProductName} Already Exist");
            }
        }

        public async Task<bool> Delete(int id)
        {
            Product productToDelete =  await _productRepository.GetProductById(id);
            if (productToDelete != null)
            {
                int deleteStatus =await _productRepository.Delete(productToDelete);
                if(deleteStatus == 1)
                {
                    return true;
                }
                else    return false;
                
            }
            else
            {
                throw new ProductDoesNotExistException($"Product with product Id {id} does not exist");
            }
        }

        public async Task<bool>  Edit(int idToUpdate, Product product)
        {
            
            Product productToUpdate = await _productRepository.GetProductById(idToUpdate);
            if(productToUpdate != null)
            {
                productToUpdate.Id=idToUpdate;
                int editStatus = await _productRepository.Edit(product, productToUpdate);
                if (editStatus == 1)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                throw new ProductDoesNotExistException($"Product with product Id {product.Id} does not exist");
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }
    }
}
