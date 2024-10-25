using CRUD.Models;
using CRUD.Service.IService;
using CRUD.Service.ServiceImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }

            await productService.CreateProductAsync(product);

            // Use the 'CreatedAtAction' method to return the newly created product.
            return CreatedAtAction(nameof(GetProductById), new { id = product.Productid }, product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(); // Returns 404 if the product is not found
            }

            return Ok(product); // Returns the product data
        }

        [HttpPut("update/{productid}")]
        public async Task<IActionResult>UpdateProduct(int productid, [FromBody] Product product)
        {
            if(productid != product.Productid)
            {
                return BadRequest("Product ID Mismatch");
            }

            var result = await productService.UpdateProductAsyc(productid, product);

            if(!result)
            {
                return NotFound("Product Not Found");
            }

            return Ok("Product Updated Successfully !!!");
        }

        [HttpDelete("delete/{productid}")]
        public async Task<IActionResult>DeleteProduct(int productid)
        {
            var result= await productService.DeleteProductAsyc(productid);

            if(!result)
            {
                return NotFound("Product Not Found");
            }

            return Ok("Product Deleted Successfully !!!");
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // Validate page number and page size
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            // Fetch paginated products
            var products = await productService.GetProductsAsync(pageNumber, pageSize);

            // Check if any products are returned
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

    }
}
