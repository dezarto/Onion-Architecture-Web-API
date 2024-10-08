namespace DezartoAPI.Application.DTOs
{
    public class UpdateProfileDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
