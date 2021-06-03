namespace Infrastructure.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        TEntity Create(TEntity entity);
        void Delete(string id);
        TEntity Get(string id);
        TEntity Update(TEntity entity);
    }
}
