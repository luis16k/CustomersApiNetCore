using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Domain.Models
{
    [Table("Purchases", Schema = "cust")]
    public class PurchaseModel
    {
        [NotMapped]
        public static double baseCost = 100;
        
        [Key]
        public int PurchaseId { set; get; }
        public double Cost { set; get; }
        public int CustomerId { set; get; }
        public CustomerModel Customer { set; get; }
    }
}
