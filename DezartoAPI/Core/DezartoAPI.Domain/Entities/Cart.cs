using DezartoAPI.Domain.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DezartoAPI.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public ObjectId CustomerId { get; set; }
    }

    public class CartItem
    {
        public ObjectId ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
