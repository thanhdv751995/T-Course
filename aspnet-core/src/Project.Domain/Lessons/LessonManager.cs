using Project.BaiHocs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Project.Lessons
{
    public class LessonManager : DomainService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonManager(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Lesson> CreateAsync(
            [NotNull] string name,
            [NotNull] string description,
            Guid IDCourse
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingLesson = await _lessonRepository.FindByNameAsync(name);
            if (existingLesson != null)
            {
                throw new LessonNameAlreadyExistsException(name);
            }
            return new Lesson(
                GuidGenerator.Create(),
                name,
                description,
                IDCourse
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Lesson lesson,
            [NotNull] string newName)
        {
            Check.NotNull(lesson, nameof(lesson));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingLesson = await _lessonRepository.FindByNameAsync(newName);
            if (existingLesson != null && existingLesson.Id != lesson.Id)
            {
                throw new LessonNameAlreadyExistsException(newName);
            }

            lesson.ChangeName(newName);
        }
    }
}
