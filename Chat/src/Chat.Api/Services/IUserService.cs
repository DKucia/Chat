using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services
{
    public interface IUserService
    {
        Task<bool> IsExist(string username);
    }
}
