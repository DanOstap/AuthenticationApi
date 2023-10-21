using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AuthenticationApi.Models
{
    public class DataBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int? Role  { get; set; }
        public bool Verification_Email { get; set; } = false;

    }
}
