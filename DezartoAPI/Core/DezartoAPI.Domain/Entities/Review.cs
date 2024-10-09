namespace DezartoAPI.Domain.Entities
{
    public class Review
    {
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; }
    }
}
