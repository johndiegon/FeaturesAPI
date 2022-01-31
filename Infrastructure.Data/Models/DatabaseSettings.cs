using FeaturesAPI.Infrastructure.Data.Interface;

namespace FeaturesAPI.Infrastructure.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ClientsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string BooksCollectionName { get; set; }
        public string ContactListCollectionName { get; set; }
        public string CategorysCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ContactCollecionName { get; set; }
        public string TypeListCollectionName { get; set; }
        public string ResumeListCollectionName { get; set; }
        public string DataDashboardCollectionName { get; set; }
        public string ChatCollectionName { get; set; }
        public string LastMessageCollectionName { get; set; }
    }
}
