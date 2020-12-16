using ContoseCrafts.Models;
using ContoseCrafts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContoseCrafts.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService)
        {
            this.ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }

        [Route("Rate")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery]string productId,
            [FromQuery]int rating )
        {
            ProductService.AddRating(productId, rating);
            return Ok();
        }
    }
}
