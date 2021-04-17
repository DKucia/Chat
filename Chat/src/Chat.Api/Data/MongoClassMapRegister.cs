using Chat.Api.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
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
                x.MapIdField(c => c.Id).SetIdGenerator(new StringObjectIdGenerator());
            });

            BsonClassMap.RegisterClassMap<Conversation>(x =>
            {
                x.AutoMap();
                x.MapIdField(c => c.Id).SetIdGenerator(new StringObjectIdGenerator());
            });

            BsonClassMap.RegisterClassMap<Message>(x =>
            {
                x.AutoMap();
                x.MapIdField(c => c.Id).SetIdGenerator(new StringObjectIdGenerator());
            });
        }
    }
}
