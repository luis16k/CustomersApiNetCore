using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Domain.Models
{
    [Table("Customers", Schema = "cust")]
    public class CustomerModel
    {
        public CustomerModel()
        {
            this.Purchases = new List<PurchaseModel>();
        }

        [Key]
        public int CustomerId { set; get; }

        [Required]
        [MaxLength(200)]
        public string? Name { set; get; }

        public List<PurchaseModel> Purchases { get; set; }
    }
}
