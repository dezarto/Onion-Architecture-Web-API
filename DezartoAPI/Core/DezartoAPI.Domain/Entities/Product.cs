using DezartoAPI.Domain.Entities.Common;

namespace DezartoAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public float Evaluation { get; set; }
        public int EvaluationAmount { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
