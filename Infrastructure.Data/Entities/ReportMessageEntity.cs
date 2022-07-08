using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities
{

    public class ReportMessageEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Template { get; set; }
        public string ClientID { get; set; }
        public string Month { get; set; }   
        public List<HistoryAnswerEntity> HistoryAnswers { get; set; }
        public List<HistorySenderEntity> HistorySenders { get; set; }
        public class HistoryAnswerEntity
        {
            public DateTime DateTime { get; set; }  
            public string Answer { get; set; }
        }
        public class HistorySenderEntity
        {
            public DateTime DateTime { get; set; }
            public string IdList { get; set; }
            public int Count { get; set; }
        }
    }
}
