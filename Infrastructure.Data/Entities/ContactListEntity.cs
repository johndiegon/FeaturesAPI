using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities
{
    public class ContactListEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<MessageEntity> ListSendMessage { get; set; }
        public List<ContactEntity> ListContact { get; set; }
    }
    public class MessageEntity
    {
        public string TextMessage { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class ContactEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
