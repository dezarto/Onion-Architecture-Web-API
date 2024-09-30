using DezartoAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public string CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalPrice => Items?.Sum(item => item.Quantity * item.UnitPrice) ?? 0;
    }

    public class CartItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
