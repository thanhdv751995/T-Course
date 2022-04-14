using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Project.DanhMucs
{
    public class CategoryManager : DomainService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateAsync(
            [NotNull] string name,
            [NotNull] string Decription,
            Guid? IDParent
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingCategory = await _categoryRepository.FindByNameAsync(name);
            if (existingCategory != null)
            {
                throw new CategoryNameAlreadyExistsException(name);
            }
            return new Category(
                GuidGenerator.Create(),
                name,
                Decription,
                IDParent
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Category category,
            [NotNull] string newName)
        {
            Check.NotNull(category, nameof(category));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingCategory = await _categoryRepository.FindByNameAsync(newName);
            if (existingCategory != null && existingCategory.Id != category.Id)
            {
                throw new CategoryNameAlreadyExistsException(newName);
            }

            category.ChangeName(newName);
        }
    }
}
