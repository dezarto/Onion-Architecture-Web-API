using MongoDB.Bson;

namespace DezartoAPI.Application.DTOs
{
    public class RegisterDTO
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public List<string> Roles { get; set; } = new List<string> { "User" };
        public int LoyaltyPoints { get; set; } = 0;
        public ObjectId CartId { get; set; } = ObjectId.GenerateNewId();
    }
}
