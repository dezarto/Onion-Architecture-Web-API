using MongoDB.Bson;

namespace DezartoAPI.Application.DTOs
{
    public class CategoryDTO
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ParentCategoryId { get; set; }
    }
}
