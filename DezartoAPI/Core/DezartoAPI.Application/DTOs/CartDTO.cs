using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Application.DTOs
{
    public class CartDTO
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public ObjectId CustomerId { get; set; }
    }

    public class CartItemDTO
    {
        public ObjectId ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
