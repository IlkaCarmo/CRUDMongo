using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApiMongoDb.Models
{
    public class Produto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nome")] 
        public string Nome { get; set; } = null;
    }
}
