namespace proj.Mappers;

public interface IMapperVMs<TViewModel, TDto>
{
    TViewModel MapToVm(TDto dto);
    TDto MapToDto(TViewModel viewModel);
}