using Customer.Domain.Models;
using Customer.Domain.Models.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Service.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerModel> Add(NameAdapter name);
        Task<CustomerModel> Edit(CustomerAdapter customer);
        Task<bool> Delete(int id);
        Task<List<CustomerModel>> GetAll();
        Task<CustomerModel> GetById(int id);
        Task<List<CustomerModel>> GetByName(string name);
    }
}
