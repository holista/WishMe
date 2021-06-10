using System;
using System.Reflection;
using MongoDB.Driver;

namespace WishMe.Service.Database
{
    public static class MongoCollectionExtensions
    {
        public static IMongoCollection<TDocument> GetCollection<TDocument>(this IMongoDatabase database)
        {
            string collectionName = GetCollectionName<TDocument>();

            return database.GetCollection<TDocument>(collectionName);
        }

        private static string GetCollectionName<TDocument>()
        {
            var attribute = typeof(TDocument).GetCustomAttribute<CollectionAttribute>();
            if (attribute == null)
                throw new InvalidOperationException();

            return attribute.Name;
        }
    }
}
