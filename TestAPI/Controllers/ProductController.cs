using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly IList<string> Products = new List<string>
        {
        "Hotel",
        "Tour",
        "Flight",
        "Transfer",
        "Package",
        "Insurance"
        };

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

       
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            if (!Products.Contains(id))
                return NotFound(id);
            return Ok(Products.Where(c=>c==id).First());
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            if (!Products.Contains(id))
                return NotFound(id);
            try
            {
                Products.Remove(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);

            }
             
        }
        [HttpPut]
        public IActionResult Put(string id)
        { 

            try
            {
                Products.Add(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);

            }
             
        }

    }
}
