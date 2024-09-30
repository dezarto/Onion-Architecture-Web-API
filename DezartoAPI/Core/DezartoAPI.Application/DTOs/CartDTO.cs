using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Application.DTOs
{
    public class CartDTO
    {
        public string? Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<CartItemDTO> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CartItemDTO
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
