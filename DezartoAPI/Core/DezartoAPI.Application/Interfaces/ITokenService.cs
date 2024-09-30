using DezartoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Customer customer);
        RefreshToken GenerateRefreshToken();
    }
}
