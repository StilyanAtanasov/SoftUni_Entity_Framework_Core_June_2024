using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Articles;

public class Article
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("author")]
    public string Author { get; set; } = null!;

    [BsonElement("date")]
    public string Date { get; set; } = null!;

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("rating")]
    public string Rating { get; set; } = null!;
}