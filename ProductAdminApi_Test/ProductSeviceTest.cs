using ProductAdminApi.Model;
using ProductAdminApi.Repository;
using ProductAdminApi.Service;

namespace ProductAdminApi_Test
{
    public class ProductServiceTest : IClassFixture<ProductDbFixture>
    {
        readonly IProductRepository _productRepository;
        readonly IProductService _productService;

        public ProductServiceTest(ProductDbFixture productDbFixture)
        {
            _productRepository = new ProductRepository(productDbFixture._productDbContext);
            _productService = new ProductService(_productRepository);
        }


        [Fact]
        public async Task GetAllProduct_ShouldReturnAllProducts()
        {
            var expected = 3;
            var actual = await _productService.GetAllProducts();
            Assert.Equal(expected, actual.Count());
        }


        [Fact]
        public void AddProduct_ShouldAddProductIfProductNotPresentAndReturnTrue()
        {
            var expected = true;
            Product product = new Product() { Id = 4, ProductName = "MSI", ImagePath = "string", Price = 78000, Category = "Laptop" };
            var actual = _productService.AddProduct(product);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Edit_ShouldEditProductIfProductPresentAndReturnTrue()
        {
            var expected = true;
            var id =2;
            Product product = new Product() { ProductName = "MSI", ImagePath = "string", Price = 78000, Category = "Laptop" };
            var actual = await _productService.Edit(id, product);
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task Delete_DeleteProductAndReturnsTrueIfPresent()
        {
            var expected = true;
            var id = 3;
            var actual = await _productService.Delete(id);
            Assert.Equal(expected, actual);
        }

    }
}
