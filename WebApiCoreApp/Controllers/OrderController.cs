using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.Models;
using WebApiCoreApp.Repos;
using WebApiCoreApp.ViewModel;

namespace WebApiCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IproductRepo productRepo;
        private readonly IOrderRepo orderRepo;

        public OrderController(IproductRepo productRepo, IOrderRepo orderRepo)
        {
            this.productRepo = productRepo;
            this.orderRepo = orderRepo;
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrderById(int id)
        {
            order o = orderRepo.GetOrderById(id);
            if (o != null)
                return Ok(o);
            else
                return NotFound();
        }

        public IActionResult GetAllOrders()
        {
            IEnumerable<order> orders = orderRepo.GetAllOrders();
            if (orders != null)
                return Ok(orders);
            else
                return NotFound();
        }


        [HttpDelete]
        public IActionResult delete(order o)
        {
            if (o != null)
            {
                orderRepo.Delete(o);
                return NoContent();
            }

            return BadRequest();

        }


        [HttpPost]
        public IActionResult AddOrder(OrderRequestVM order)
        {
            if (ModelState.IsValid)
            {
                order o = orderRepo.orderRequest(order);
                return Ok(order);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
