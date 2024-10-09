using DezartoAPI.Domain.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        public decimal TotalAmount { get; set; }
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
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
