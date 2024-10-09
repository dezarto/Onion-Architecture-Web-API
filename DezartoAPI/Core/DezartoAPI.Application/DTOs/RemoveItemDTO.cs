using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DezartoAPI.Application.DTOs
{
    public class RemoveItemDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
    }
}
