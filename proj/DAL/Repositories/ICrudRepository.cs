namespace DAL.Repositories;

public interface ICrudRepository<TEntity>
{
    int Create(TEntity entity);
    TEntity Read(int? id);
    void Update(TEntity entity);
    void Delete(int? id);
}