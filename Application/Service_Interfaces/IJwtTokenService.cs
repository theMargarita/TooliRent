using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service_Interfaces
{
    public interface IJwtTokenService
    {
        string CreateToken(IdentityUser user, IEnumerable<string>? roles = null, int? customerId = null);
    }
}
