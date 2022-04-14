using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Project.KhoaHocs
{
    public class CourseManager : DomainService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseManager(ICourseRepository courceRepository)
        {
            _courseRepository = courceRepository;
        }

        public async Task<Course> CreateAsync(
            [NotNull ] string name,
            [NotNull] string description,
            [NotNull] string benefit,
            Guid IDCategory,
            Guid IDUser
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingCourse = await _courseRepository.FindByNameAsync(name);
            if (existingCourse != null)
            {
                throw new CourseNameAlreadyExistsException(name);
            }
            return new Course(
                GuidGenerator.Create(),
                name,
                description,
                benefit,
                IDCategory,
                IDUser
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Course cource,
            [NotNull] string newName)
        {
            Check.NotNull(cource, nameof(cource));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingCource = await _courseRepository.FindByNameAsync(newName);
            if (existingCource != null && existingCource.Id != cource.Id)
            {
                throw new CourseNameAlreadyExistsException(newName);
            }

            cource.ChangeName(newName);
        }
    }
}
