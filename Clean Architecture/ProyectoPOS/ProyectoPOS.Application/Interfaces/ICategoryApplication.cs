using POS.Application.Commons.Bases;
using POS.Application.Dtos;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<BaseEntityResponse<CategoryResponseDTO>>> ListCategories(BaseFiltersRequest filters);

        Task<BaseResponse<IEnumerable<CategorySelectResponseDTO>>> ListSelectCategories();

        Task<BaseResponse<CategoryResponseDTO>> CategoryById(int categoryId);

        Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDTO categoryRequestDTO);

        Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDTO categoryRequestDTO);

        Task<BaseResponse<bool>> RemoveCategory(int categoryId);
    }
}