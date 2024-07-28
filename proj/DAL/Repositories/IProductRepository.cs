using DAL.Entities;

namespace DAL.Repositories;

public interface IProductRepository : ICrudRepository<ProductEntity>
{
    IQueryable<ProductEntity> GetOutOfStock();
    void DecrementStock(ProductEntity entity ,int count);
    void IncrementStock(ProductEntity entity, int count);
}