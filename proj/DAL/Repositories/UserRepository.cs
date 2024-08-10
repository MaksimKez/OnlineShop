using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public int Create(UserEntity userEntity)
    {
        var user = dbContext.Users.Add(userEntity);
        dbContext.SaveChanges();
        return user.Entity.Id;
    }

    public UserEntity? Read(int? id)
    {
        var user = dbContext.Users.FirstOrDefault(us => us.Id == id);
        return user;
    }

    public void Update(UserEntity userEntity)
    {
        dbContext.Update(userEntity);
        dbContext.SaveChanges();
    }

    public void Delete(int? id)
    {
        var user = dbContext.Users.FirstOrDefault(us => us.Id == id);
        if (user == null)
            throw new ArgumentException(nameof(id));
        dbContext.Users.Remove(user);
        dbContext.SaveChanges();
    }

    public IQueryable<UserEntity> GetAllWithoutOrders()
    {
        return dbContext.Users
            .Where(us => us.Orders == null || us.Orders.Count == 0);
    }
}