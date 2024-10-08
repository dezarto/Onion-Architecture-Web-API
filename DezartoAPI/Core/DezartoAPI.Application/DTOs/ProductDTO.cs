using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DezartoAPI.Application.DTOs
{
    public class ProductDTO
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public float Evaluation { get; set; }
        public int EvaluationAmount { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrdersId { get; set; }
    }
}
