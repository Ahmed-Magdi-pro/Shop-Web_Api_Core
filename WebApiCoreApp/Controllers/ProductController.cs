using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.Models;
using WebApiCoreApp.Repos;

namespace WebApiCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IproductRepo _productRepo;

        public ProductController(IproductRepo productRepo)
        {
            _productRepo = productRepo;
        }


        public IActionResult GetAll()
        {
            IEnumerable<product> model = _productRepo.GetAllproducts();
            if (model != null)
            {
                return Ok(model);
            }
            else return NotFound();
        }

        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            product pro= _productRepo.GetproductByID(id);
            if (pro != null)
                return Ok(pro);
            else
                return NotFound();
        }

        [HttpDelete]
        public IActionResult Delete(product product)
        {
            if (product != null)
            {
                _productRepo.Delete(product);
                return NoContent();
            }
            else
                return BadRequest();
        }


        [HttpPost]
        public IActionResult AddProduct(product pro)
        {
            if (pro != null)
            {
                _productRepo.Addproduct(pro);
                return Created("AddProduct", pro);
            }
            else
                return BadRequest();
        }


        [HttpPut]
        public IActionResult UpdateProduct(product pro)
        {
            if (pro != null)
            {
                _productRepo.Update(pro);
                return Ok(pro);
            }
            else
                return BadRequest();
        }

    }
}
