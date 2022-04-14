using Microsoft.AspNetCore.Authorization;
using Project.Attachments;
using Project.DanhMucs;
using Project.KhoaHocs;
using Project.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Project.DanhMucs
{
    public class CategoryAppService : ProjectAppService, ICategoryAppService,IRepositoryShare<CategoryDto,Guid>,IRepositoryCreateUpdateShare<CreateCategoryDto,Guid>,IGetListShareFilter<CategoryDto,GetCategoryListDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryManager _categoryManager;
        private readonly IRepository<Attachment, Guid> _attachmentRepository;

        public CategoryAppService(
            ICategoryRepository categoryRepository,
            CategoryManager categoryManager,
            IAttachmentRepository attachmentRepository)
        {
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<CategoryDto> GetAsyncById(Guid id)
        {
            var category = await _categoryRepository.GetAsync(id);
            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task<CategoryDto> GetAsync(Guid id)
        {
            //var category = await _categoryRepository.GetAsync(id);
            //return ObjectMapper.Map<Category, CategoryDto>(category);
            var queryable = await _categoryRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from attachment in _attachmentRepository
                        join category in queryable on attachment.IDTable equals category.Id
                        where category.Id == id
                        select new { category, attachment };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Category), id);
            }

            var categoryeDto = ObjectMapper.Map<Category, CategoryDto>(queryResult.category);
            categoryeDto.URL = queryResult.attachment.URL;
            return categoryeDto;
        }

        public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
        {
            var queryable = await _categoryRepository.GetQueryableAsync();
            var query = from cat in queryable
                        join c in _categoryRepository on cat.IDParent equals c.Id into ps
                        from p in ps.DefaultIfEmpty()
                        select new { cat, Parentname = p == null ? null : p.Name };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var catDtos = queryResult.Select(x =>
            {
                var catDto = ObjectMapper.Map<Category, CategoryDto>(x.cat);
                catDto.ParentName = x.Parentname;
                return catDto;
            }).ToList();
            var totalCount = await _categoryRepository.CountAsync();

            return new PagedResultDto<CategoryDto>(
                totalCount,
                catDtos
            );
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
        {
            var category = await _categoryManager.CreateAsync(
                input.Name,
                input.Description,
                input.IDParent
            );

            await _categoryRepository.InsertAsync(category);

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task UpdateAsync(Guid id, UpdateCategoryDto input)
        {
            if (input.IDParent != null)
            {
                var cat = await _categoryRepository.FirstAsync(x => x.Id == input.IDParent);
                if (cat.IDParent == id)
                {
                    throw new UserFriendlyException("Danh mục con không thể dùng làm danh mục cha !!", "You are trying to see a product that is deleted...");
                }
            }

            var category = await _categoryRepository.GetAsync(id);
            if (category.Id == input.IDParent)
            {
                throw new UserFriendlyException("Bản thân không thể làm cha được !!", "You are trying to see a product that is deleted...");
            }
            if (category.Name != input.Name)
            {
                await _categoryManager.ChangeNameAsync(category, input.Name);
            }

            category.Description = input.Description;
            category.IDParent = input.IDParent;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
            var cat = _categoryRepository.Where(c => c.IDParent == id).ToArray();
            if (cat.Length > 0)
            {
                for (int i = 0; i < cat.Length; i++)
                {
                    await _categoryRepository.DeleteAsync(cat[i].Id);
                }
            }
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync()
        {
            var categories = await _categoryRepository.GetListAsync();

            return new ListResultDto<CategoryLookupDto>(
                ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories)
            );
        }

        public async Task<PagedResultDto<CategoryDto>> GetAllListCategoryAsync(GetCategoryListDto input)
        {
            var queryable = await _categoryRepository.GetQueryableAsync();
            var query = from cat in queryable
                        join c in _categoryRepository on cat.IDParent equals c.Id into ps
                        from p in ps.DefaultIfEmpty()
                        select new { cat, Parentname = p == null ? null : p.Name };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var catDtos = queryResult.Select(x =>
            {
                var catDto = ObjectMapper.Map<Category, CategoryDto>(x.cat);
                catDto.ParentName = x.Parentname;
                return catDto;
            }).ToList();
            var totalCount = await _categoryRepository.CountAsync();

            return new PagedResultDto<CategoryDto>(
                totalCount,
                catDtos
            );
        }

        public async Task<CategoryDto> GetByIDCategoryAsync(Guid id)
        {
            //var category = await _categoryRepository.GetAsync(id);
            //return ObjectMapper.Map<Category, CategoryDto>(category);
            var queryable = await _categoryRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from attachment in _attachmentRepository
                        join category in queryable on attachment.IDTable equals category.Id
                        where category.Id == id
                        select new { category, attachment };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Category), id);
            }

            var categoryeDto = ObjectMapper.Map<Category, CategoryDto>(queryResult.category);
            categoryeDto.URL = queryResult.attachment.URL;
            return categoryeDto;
        }


        public async Task DeleteCategoryByIDAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
            var cat = _categoryRepository.Where(c => c.IDParent == id).ToArray();
            if (cat.Length > 0)
            {
                for (int i = 0; i < cat.Length; i++)
                {
                    await _categoryRepository.DeleteAsync(cat[i].Id);
                }
            }
        }


        public async Task UpdateCategoryAsync(CreateCategoryDto input, Guid id)
        {
            if (input.IDParent != null)
            {
                var cat = await _categoryRepository.FirstAsync(x => x.Id == input.IDParent);
                if (cat.IDParent == id)
                {
                    throw new UserFriendlyException("Danh mục con không thể dùng làm danh mục cha !!", "You are trying to see a product that is deleted...");
                }
            }

            var category = await _categoryRepository.GetAsync(id);
            if (category.Id == input.IDParent)
            {
                throw new UserFriendlyException("Bản thân không thể làm cha được !!", "You are trying to see a product that is deleted...");
            }
            if (category.Name != input.Name)
            {
                await _categoryManager.ChangeNameAsync(category, input.Name);
            }

            category.Description = input.Description;
            category.IDParent = input.IDParent;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task PostInsertCategoryAsync(CreateCategoryDto Entity)
        {
            var category = await _categoryManager.CreateAsync(
               Entity.Name,
               Entity.Description,
               Entity.IDParent
           );

            await _categoryRepository.InsertAsync(category);
        }





        //...SERVICE METHODS WILL COME HERE...
    }
}