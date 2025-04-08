using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase

    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products=await _productService.GetProductsAsync();
            if(products.Count>0){
                return Ok(products);
            }else{
                return NotFound("Ürün Bulunamadı"); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product=await _productService.GetProductAsync(id);
            if(product!=null){
                return Ok(product);
            }else{
                return NotFound("Ürün Bulunamadı");
            }
          
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetProductAdmin(int UserId)
        {
            var adminProducts=await _productService.GetProductAdminAsync(UserId);
            if(adminProducts.Count>0){
                return Ok(adminProducts);
            }else{  
                return NotFound("Ürün Bulunamadı");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddProducts([FromBody] Product product)
        {
            await _productService.AddProductAsync(product);
            return Ok(new { message = "Ürün Eklendi", product });
        }



        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.DeleteProductAsync(id);
            if (product == null)
            {
                return NotFound("Ürün Bulunamadı");
            }

            await _productService.DeleteProductAsync(id);
            return Ok(new { message = "Ürün silindi" });


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product updateProduct)
        {
            var product = await _productService.UpdateProductAsync(id, updateProduct);
            if (product == null)
            {
                return NotFound("Ürün Bulunamadı");
            }
            await _productService.UpdateProductAsync(id, updateProduct);    
            return Ok(new { message = "Ürün güncellendi", product });
        }
    }
}

