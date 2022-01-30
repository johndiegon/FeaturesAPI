namespace FeaturesAPI.Infrastructure.Data.Interface
{
    public interface IDatabaseSettings
    {
        string BooksCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ClientsCollectionName { get; set; }
        string DataDashboardCollectionName { get; set; }
        string ContactListCollectionName { get; set; }
        string CategorysCollectionName { get; set; }
        string ContactCollecionName { get; set; }
        string ConnectionString { get; set; }
        string TypeListCollectionName { get; set; }
        string ResumeListCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
  
}
