using DAL.Entities;

namespace DAL.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
{
    public int Create(ProductEntity entity)
    {
        var product = dbContext.Products.Add(entity);
        dbContext.SaveChanges();
        return product.Entity.Id;
    }

    public ProductEntity Read(int? id)
    {
        var product = dbContext.Products.FirstOrDefault(pr => pr.Id == id);
        return product ?? throw new ArgumentException(nameof(id));
    }

    public void Update(ProductEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int? id)
    {
        var product = dbContext.Products.FirstOrDefault(us => us.Id == id);
        if (product == null)
            throw new ArgumentException(nameof(id));
        dbContext.Products.Remove(product);
        dbContext.SaveChanges();
    }

    public IQueryable<ProductEntity> GetOutOfStock()
    {
        return dbContext.Products.Where(pr => pr.StockQuantity == 0);
    }

    public void DecrementStock(ProductEntity entity, int count)
    {
        if (entity.StockQuantity-count < 0)
            throw new ArgumentException("Out Of Stock!", nameof(count));
        entity.StockQuantity -= count;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }

    public void IncrementStock(ProductEntity entity, int count)
    {
        entity.StockQuantity += count;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}