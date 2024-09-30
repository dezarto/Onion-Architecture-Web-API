using DezartoAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string Adress { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingAddressId { get; set; }
        public string BillingAddressId { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
        public Customer Customer { get; set; }
        //public ICollection<Product> Products { get; set; } = new List<Product>();
        //public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
