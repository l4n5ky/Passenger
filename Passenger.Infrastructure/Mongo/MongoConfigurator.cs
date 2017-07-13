using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Mongo
{
    public static class MongoConfigurator
    {
        private static bool initialized;

        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }
            RegisterConventions();

        }

        private static void RegisterConventions()
        {
            ConventionRegistry.Register("PassengerConventions", new MongoConventions(), x => true);
            initialized = true;
        }

        private class MongoConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
