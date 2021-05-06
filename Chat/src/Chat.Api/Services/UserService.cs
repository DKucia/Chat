using Chat.Api.Data;
using Chat.Api.Domain;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoProvider<User> _mongoProvider;

        public UserService(IMongoProvider<User> mongoProvider)
        {
            _mongoProvider = mongoProvider;
        }
        public Task<bool> IsExist(string username)
        {
            return _mongoProvider.Querable.AnyAsync(c => c.Username.Equals(username));
        }
    }
}
