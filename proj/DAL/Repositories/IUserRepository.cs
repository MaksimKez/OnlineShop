using DAL.Entities;

namespace DAL.Repositories;

public interface IUserRepository : ICrudRepository<UserEntity>
{
    IQueryable<UserEntity> GetAllWithoutOrders();
    
}