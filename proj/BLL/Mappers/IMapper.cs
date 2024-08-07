namespace BLL.Mappers;

public interface IMapper<TDto, TEntity>
{
    TDto? MapToModel(TEntity? entity);
    TEntity? MapFromModel(TDto? dto);
}