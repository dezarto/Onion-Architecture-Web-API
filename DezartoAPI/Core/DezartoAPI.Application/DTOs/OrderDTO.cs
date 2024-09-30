using MongoDB.Bson;

namespace DezartoAPI.Application.DTOs
{
    public class OrderDTO
    {
        public string? Id { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingAddressId { get; set; }
        public string BillingAddressId { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
        public ObjectId CustomerId { get; set; }
        public List<ProductInOrderDTO> Products { get; set; }
    }

    public class ProductInOrderDTO
    {
        public ObjectId ProductId { get; set; } // Ürün referansı
        public int Quantity { get; set; } // Sipariş edilen ürün miktarı
    }
}
