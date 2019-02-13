using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SodaMachine.Domain;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Models.Request;
using SodaMachine.Services;

namespace SodaMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IOrderService OrderService;

        public OrderController(ApplicationContext context,
            IOrderService orderService)
        {
            _context = context;
            OrderService = orderService;
        }
        
        [HttpPost]
        public async Task<IActionResult> ProcessOrder([FromBody]OrderRequest request)
        {
            // разница между закинутыми монетами и суммой покупки
            var orderIsValid = (request.Coins.Select(x => x.Value).Sum() - request.Sodas.Select(x => x.Price).Sum()) >= 0;
            // доп проверка
            if (orderIsValid)
            {
                var change = await OrderService.ProcessOrder(request);

                return Ok(change);
            }

            return BadRequest("Order not valid");
        }
    }
}