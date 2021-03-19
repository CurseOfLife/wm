using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IAuthManagerService
    {
        Task<string> CreateToken();
        Task<bool> ValidateUser(LoginUserDTO userDTO);     
    }
}
