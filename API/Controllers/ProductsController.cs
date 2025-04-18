using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class ProductsController(IProductRepository repo): ControllerBase
    {
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await repo.GetProductsAsync());

        }
        [HttpGet("{id:int}")] 
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
           var Product = await repo.GetProductAsync(id);
            if (Product == null) { return NotFound(); }

                return Product;
        }
        [HttpPost]
      public async Task<ActionResult<Product>>CreateProduct(Product Product)
        {
           repo.CreateProduct(Product);
            return  Product;   
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult <Product>> UpdateProduct(int id  ,Product Product)
        {
          if (Product.Id != id || ProductExists(id)) return BadRequest("Cannot Update This Product");
           repo.UpdateProduct(Product);
          if( await repo.SaveChangesAsync()) return NoContent();
            return BadRequest("problem in update");

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetProductAsync(id);
            if (product == null) { return NotFound(); };
            repo.DeleteProduct(product);
            if (await repo.SaveChangesAsync()) return NoContent();
            return BadRequest("problem in delete");
        }


        private bool ProductExists(int id)
        {
            return repo.ProductExists(id);
        }

    }
}
