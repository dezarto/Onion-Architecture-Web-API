using DezartoAPI.Domain.Entities.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = "User"; // Default role
        public int LoyaltyPoints { get; set; } = 0;  // Default loyalty points
        public List<ObjectId> OrderIds { get; set; }
    }

    public class Address
    {
        public string NameOfAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
