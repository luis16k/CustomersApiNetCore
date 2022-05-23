using Customer.Domain.Models;
using Customer.Domain.Models.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Service.Contracts
{
    public interface IPurchaseService
    {
        Task<double> Add(PurchaseAdapter idCustomer);
        Task<PurchaseModel> GetById(int idPurchase);
        Task<bool> Remove(int idPurchase);
        Task<List<PurchaseModel>> GetByCustomer(int idCustomer);
    }
}
