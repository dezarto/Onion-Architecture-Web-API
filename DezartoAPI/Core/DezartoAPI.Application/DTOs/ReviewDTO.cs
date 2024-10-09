using MongoDB.Bson;

namespace DezartoAPI.Application.DTOs
{
    public class ReviewDTO
    {
        public ObjectId Id { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
