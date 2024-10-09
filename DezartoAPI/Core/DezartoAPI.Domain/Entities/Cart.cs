using DezartoAPI.Domain.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DezartoAPI.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public DateTime UpdatedDate { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CustomerId { get; set; }
    }

    public class CartItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
