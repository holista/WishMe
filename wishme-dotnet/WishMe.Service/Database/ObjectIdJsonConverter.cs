using System;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace WishMe.Service.Database
{
    public class ObjectIdJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ObjectId) || objectType == typeof(ObjectId?);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (reader.TokenType == JsonToken.Null && objectType == typeof(ObjectId?))
                return null;

            if (reader.TokenType != JsonToken.String)
                throw new InvalidOperationException();

            var value = (string?)reader.Value;

            return ObjectId.TryParse(value, out var objectId)
                ? objectId
                : throw new JsonException();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (value is ObjectId objectId)
                writer.WriteValue(objectId.ToString());
            else
                throw new ArgumentException();
        }
    }
}
