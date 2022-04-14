using Project.KhoaHocs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.DanhMucs
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<CategoryDto> GetAsync(Guid id);

        Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input);

        Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync();

        Task<CategoryDto> CreateAsync(CreateCategoryDto input);

        Task UpdateAsync(Guid id, UpdateCategoryDto input);

        Task DeleteAsync(Guid id);

    }
}