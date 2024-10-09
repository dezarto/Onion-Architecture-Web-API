using DezartoAPI.Domain.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DezartoAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string SKU { get; set; }
        public int StockQuantity { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public double Weight { get; set; }
        public double Dimensions { get; set; }
        public double Rating { get; set; }
        public int TotalReviews { get; set; }
        public bool IsActive { get; set; }


        public string CategoryId { get; set; }
        public List<string> TagIds { get; set; }
        public List<string> ReviewIds { get; set; }
        public List<string> DiscountIds { get; set; }
    }
}
