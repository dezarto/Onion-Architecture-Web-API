namespace DezartoAPI.Application.DTOs
{
    public class CustomerDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }
        public int LoyaltyPoints { get; set; }  // Sadakat puanları
    }
}
