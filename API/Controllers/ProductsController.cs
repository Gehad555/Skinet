using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class ProductsController : Controller
    {
        private readonly StoreContext Context;
        public ProductsController(StoreContext Context)
        {
            this.Context = Context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
           return await Context.Products.ToListAsync();

        }
        [HttpGet("{id:int}")] 
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
           var Product = await Context.Products.FindAsync(id);
            if (Product == null) { return NotFound(); }

                return Product;
        }
        [HttpPost]
      public async Task<ActionResult<Product>>CreateProduct(Product Product)
        {
            Context.Products.Add(Product);
            await Context.SaveChangesAsync();
            return Product;
        }
        [HttpPut("{id : int}")]
        public async Task<ActionResult <Product>> UpdateProduct(int id  ,Product Product)
        {
            if (Product.Id != id || ProductExists(id)) return BadRequest("Cannot Update This Product");
            Context.Entry(Product).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await Context.Products.FindAsync(id);
            if (product == null) { return NotFound(); };
            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
            return NotFound();
        }


        private bool ProductExists(int id)
        {
            return Context.Products.Any(p => p.Id == id);
        }

    }
}
