using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAdminApi.Exceptions;
using ProductAdminApi.Model;
using ProductAdminApi.Repository;
using ProductAdminApi.Service;

namespace ProductAdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;
        readonly IProductRepository _productRepository;
        public ProductController(IProductService productService,IProductRepository productRepository)
        {
            _productService = productService; 
            _productRepository = productRepository;
        }

        [Route("GetAllProducts")]
        [HttpGet]
        public async Task<ActionResult> GetAllProdcuts()
        {
            List<Product> products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [Route("AddProduct")]
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            try
            {
                bool addProductStatus = _productService.AddProduct(product);
                return Ok(addProductStatus);
            }
            catch (ProductAlreadyExistException ex)
            {
                return StatusCode(500, ex.Message);
            }   
            
        }

        [Route("GetProductById")]
        [HttpGet]
        public async Task<ActionResult> GetProductById(int id)
        {
            var getProduct = await _productRepository.GetProductById(id)
;
            if (getProduct == null)
            {
                return BadRequest($"Product with Id {id} not found");
            }
            else
                return Ok(getProduct);
        }
        [Route("DeleteProduct/{id:int}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                bool deleteProductStatus = await _productService.Delete(id);
                return Ok(deleteProductStatus);
            }
            catch (ProductDoesNotExistException ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [Route("Edit/{idToUpdate:int}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int idToUpdate,Product product)
        {
            try
            {
                bool editStatus = await _productService.Edit(idToUpdate, product);
                return Ok(editStatus);               
            }
            catch(ProductDoesNotExistException ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }
    }
}
