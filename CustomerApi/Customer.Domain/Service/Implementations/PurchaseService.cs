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
    public class PurchaseService : IPurchaseService
    {
        private readonly ILogger<PurchaseService> _logger;
        private readonly DbContextApi _context;

        public PurchaseService(ILogger<PurchaseService> logger, DbContextApi context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Remove(int idPurchase)
        {
            try {

                var purchase = await _context.PurchaseModel.FindAsync(idPurchase).ConfigureAwait(false);

                if(purchase != null)
                {
                    _context.PurchaseModel.Remove(purchase);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    
                    return true;
                }   
                
                return false;
            }
            catch(Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        public async Task<double> Add(PurchaseAdapter idCustomer)
        {
            try {
                
                var customer = await _context.CustomerModel.Include(x => x.Purchases).Where(x => x.CustomerId == idCustomer.CustomerId).FirstOrDefaultAsync().ConfigureAwait(false);
                double cost = 0;
                
                if(customer != null)
                {
                    cost = CalculateCost(customer.Purchases.Count, PurchaseModel.baseCost);

                    var newPurchase = new PurchaseModel()
                    {
                        Customer = customer,
                        CustomerId = customer.CustomerId,
                        Cost = cost
                    };

                    customer.Purchases.Add(newPurchase);

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }

                return cost;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        public async Task<List<PurchaseModel>> GetByCustomer(int idCustomer)
        {
            try {
                var customer = await _context.CustomerModel.Include(x => x.Purchases).Where(x => x.CustomerId == idCustomer).FirstOrDefaultAsync().ConfigureAwait(false);

                if (customer == null)
                    return null;

                return customer?.Purchases?.ToList();                
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        public async Task<PurchaseModel> GetById(int idPurchase)
        {
            try {            
                var purchase = await _context.PurchaseModel.FindAsync(idPurchase).ConfigureAwait(false);
                
                return purchase;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        private double CalculateCost(int quantity, double cost)
        {
            switch (quantity)
            {
                case 1:
                case 2: return cost * .99;
                case 3:
                case 4: return cost * .98;
                case 5: 
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case > 10: return cost * .95;
                case 0:
                default: return cost;
            }
        }
    }
}
