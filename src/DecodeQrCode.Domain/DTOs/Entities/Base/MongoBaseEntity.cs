using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DecodeQrCode.Domain.DTOs.Entities.Base;

public class MongoBaseEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
