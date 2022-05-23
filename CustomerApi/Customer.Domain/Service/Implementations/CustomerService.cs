using Customer.Domain.Models;
using Customer.Infrastructure;
using Customer.Domain.Service.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Customer.Domain.Models.Adapters;

namespace Customer.Domain.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly DbContextApi _context;

        public CustomerService(ILogger<CustomerService> logger, DbContextApi context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            try {
                
                var customer = await _context.CustomerModel.FindAsync(id).ConfigureAwait(false);

                if (customer != null)
                {
                    _context.CustomerModel.Remove(customer);
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        public async Task<CustomerModel> Add(NameAdapter name)
        {
            try {

                var customer = new CustomerModel()
                {
                    Name = name.Name
                };

                await _context.CustomerModel.AddAsync(customer);
                await _context.SaveChangesAsync();
                
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        public async Task<CustomerModel> Edit(CustomerAdapter customerEdit)
        {
            try {

                var customer = new CustomerModel();

                if(customerEdit?.Id > 0)
                {
                    customer = await _context.CustomerModel.FindAsync(customerEdit.Id).ConfigureAwait(false);

                    if(customer != null)
                    {
                        customer.Name = customerEdit.Name;
                        await _context.SaveChangesAsync();
                    }
                }
                                
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        public async Task<List<CustomerModel>> GetAll()
        {
            try {

                return await _context.CustomerModel.Include(x => x.Purchases).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        public async Task<CustomerModel> GetById(int id)
        {
            try {

                return await _context.CustomerModel.Include(x => x.Purchases).Where(x => x.CustomerId == id).FirstOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        public async Task<List<CustomerModel>> GetByName(string name)
        {
            try {

                return _context.CustomerModel.Where(x => x.Name == name)?.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }
    }
}
