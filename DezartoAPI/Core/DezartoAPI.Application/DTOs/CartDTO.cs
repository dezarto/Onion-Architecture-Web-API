using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DezartoAPI.Application.DTOs
{
    public class CartDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<CartItemDTO> Items { get; set; }
        public decimal TotalPrice { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CustomerId { get; set; }
    }

    public class CartItemDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
