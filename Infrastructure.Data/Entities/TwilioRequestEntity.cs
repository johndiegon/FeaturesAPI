using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Twilio.AspNet.Common;

namespace Infrastructure.Data.Entities
{

    public class TwilioRequestEntity 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public SmsRequest Request { get; set; }
    }
}
