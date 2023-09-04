using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities
{
    public class ResumeContactListEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string IdClient { get; set; }

        public bool FileIsProcessing { get; set; }

        public DateTime InputFile { get; set; }
        public List<ContactListEntity> ContactLists { get; set; }
    }
}
