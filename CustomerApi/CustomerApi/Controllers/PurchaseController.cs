using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Domain.Models.Adapters;
using Customer.Domain.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        [Route("health")]
        public IActionResult GetHealth()
        {
            return Ok();
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> AddPurchaseAsync([FromBody] PurchaseAdapter customerId)
        {
            if (customerId.CustomerId == 0)
                return BadRequest();

            var cost = await _purchaseService.Add(customerId).ConfigureAwait(false);

            if(cost != 0)
            {
                return Ok(cost);
            }
            else
            {
                return NotFound("Customer not found");
            }

            
        }

        [HttpGet]
        [Route("customer/{customerId}")]
        public async Task<IActionResult> GetPurchasesByCustomer(int customerId)
        {
            if (customerId == 0)
                return BadRequest();

            var purchases = await _purchaseService.GetByCustomer(customerId).ConfigureAwait(false);
            
            if (purchases == null)
                return NotFound();

            return Ok(purchases);
        }

        [HttpDelete]
        [Route("id/{purchaseId}")]
        public async Task<IActionResult> RemovePurchaseByCustomer(int purchaseId)
        {
            if(purchaseId == 0)
                return BadRequest();

            return Ok(await _purchaseService.Remove(purchaseId));
        }

    }
}