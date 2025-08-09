using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpazaStock.Server.Models;
using SpazaStock.Server.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpazaStock.Server.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly SpazaStockDbContext _context;
        private readonly GroceryApiService _groceryApiService;

        public ProductsController(SpazaStockDbContext context, GroceryApiService groceryApiService)
        {
            _context = context;
            _groceryApiService = groceryApiService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPost("import-demo")]
        public async Task<IActionResult> ImportDemoProducts([FromQuery] string query = "vegetables")
        {
            var usdToZar = await _groceryApiService.GetUsdToZarRateAsync();
            var amazonProducts = await _groceryApiService.FetchAmazonProductsAsync(query);
            var walmartProducts = await _groceryApiService.FetchWalmartProductsAsync(query);
            var allProducts = amazonProducts.Concat(walmartProducts).ToList();

            var spazaMarkup = 1.45m; // 45% markup
            var demoProducts = allProducts.Select(p => new Product
            {
                ProductName = p.title,
                Category = "Groceries", // You can improve mapping later
                CostPrice = decimal.Round(p.price * usdToZar, 2),
                SellingPrice = decimal.Round(p.price * usdToZar * spazaMarkup, 2),
                CurrentStock = 10,
                MinimumStock = 5,
                IsFromDemo = true
            }).ToList();

            _context.Products.AddRange(demoProducts);
            await _context.SaveChangesAsync();
            return Ok(new { imported = demoProducts.Count });
        }
    }
}
