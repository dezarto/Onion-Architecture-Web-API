using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DezartoAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
