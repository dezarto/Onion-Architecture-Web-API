using DezartoAPI.Domain.Entities.Common;
using MongoDB.Bson;
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
        public ObjectId CustomerId { get; set; }
        public List<ProductInOrder> Products { get; set; }
    }

    public class ProductInOrder
    {
        public ObjectId ProductId { get; set; } // Ürün referansı
        public int Quantity { get; set; } // Sipariş edilen ürün miktarı
    }
}
