namespace FeaturesAPI.Infrastructure.Data.Interface
{
    public interface IDatabaseSettings
    {
        string BooksCollectionName { get; set; }
        string ClientsCollectionName { get; set; }
        string CategorysCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
  
}
