using Chat.Api.Domain;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Data
{
    public static class MongoClassMapRegister 
    {
        public static void RegisterMapping()
        {
            BsonClassMap.RegisterClassMap<User>(x =>
            {
                x.AutoMap();
                x.MapIdField(c => c.Id);
            });
        }
    }
}
