using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Domain.Models;
using Customer.Domain.Service.Contracts;
using Customer.Domain.Service.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Customer.Domain.Models.Adapters;

namespace CustomerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("health")]
        public IActionResult GetHealth()
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddCustomer([FromBody]NameAdapter name)
        {
            if (String.IsNullOrWhiteSpace(name.Name))
                return BadRequest();

            var customer = await _customerService.Add(name).ConfigureAwait(false);
            return Ok(customer);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> EditCustomer([FromBody] CustomerAdapter customer)
        {
            if(customer != null)
            {
                var cust = await _customerService.Edit(customer).ConfigureAwait(false);
                
                if(cust != null && cust.CustomerId > 0)
                {
                    return Ok(cust);
                }
                else
                {
                    return NotFound();
                }
                
            }
            
            return BadRequest();

        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetCostumers()
        {
            
            return Ok(await _customerService.GetAll().ConfigureAwait(false));
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> SearchCostumerByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty");

            var customers = await _customerService.GetByName(name).ConfigureAwait(false);

            if(customers == null || customers.Count == 0)
                return NotFound();

            return Ok(customers);
        }

        [HttpGet]
        [Route("id/{idCustomer}")]
        public async Task<IActionResult> GetCostumerById(int idCustomer)
        {
            if (idCustomer == 0)
                return BadRequest();

            var customer = await _customerService.GetById(idCustomer).ConfigureAwait(false);

            if(customer == null)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Route("id/idCustomer")]
        public async Task<IActionResult> DeleteCustomer(int idCustomer)
        {
            if (idCustomer == 0)
                return BadRequest();

            if (await _customerService.Delete(idCustomer).ConfigureAwait(true))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}