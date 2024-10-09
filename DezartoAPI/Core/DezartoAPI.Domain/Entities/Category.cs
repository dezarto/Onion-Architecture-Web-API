namespace DezartoAPI.Domain.Entities
{
    public class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ParentCategoryId { get; set; }
    }
}
