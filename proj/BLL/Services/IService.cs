using BLL.Dtos;

namespace BLL.Services;

public interface IService<TDto>
{
    int Create(TDto dto);
    TDto Get(int? id);
    void Update(TDto dto);
    void Delete(int? id);
}