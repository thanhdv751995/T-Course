using Microsoft.AspNetCore.Authorization;
using Project.Attachments;
using Project.BaiHocs;
using Project.KhoaHocs;
using Project.Permissions;
using Project.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Project.Lessons
{
    public class LessonAppService : ProjectAppService, ILessonAppService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly LessonManager _lessonManager;
        private readonly AttachmentManager _attManager;
        private readonly IRepository<Attachment, Guid> _attachmentRepository;
        private readonly IRepository<Course, Guid> _courseRepository;

        public LessonAppService(
           ILessonRepository lessonRepository,
           LessonManager lessonManager,
           IRepository<Course, Guid> courseRepository,
           IAttachmentRepository attachmentRepository,
           AttachmentManager attManager)
        {
            _lessonRepository = lessonRepository;
            _lessonManager = lessonManager;
            _courseRepository = courseRepository;
            _attachmentRepository = attachmentRepository;
            _attManager = attManager;
        }

        public async Task<LessonDto> CreateAsync(CreateLessonDto input)
        {
            var lesson = await _lessonManager.CreateAsync(
                 input.Name,
                 input.Description,
                 input.IDCourse
    ); ;

            await _lessonRepository.InsertAsync(lesson);

            var att = await _attManager.CreateAsync(
                 input.URL,
                 lesson.Id
    ); ;

            await _attachmentRepository.InsertAsync(att);

            return ObjectMapper.Map<Lesson, LessonDto>(lesson);
        }

        //[Authorize(ProjectPermissions.Lessons.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _lessonRepository.DeleteAsync(id);
            var att = _attachmentRepository.Where(l => l.IDTable == id).ToArray();
            if (att.Length > 0)
            {
                for (int i = 0; i < att.Length; i++)
                {
                    await _attachmentRepository.DeleteAsync(att[i].Id);
                }
            }
        }

        public async Task<LessonDto> GetAsync(Guid id)
        {//Get the IQueryable<Book> from the repository
            var queryable = await _courseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from course in queryable
                        join lesson in _lessonRepository on course.Id equals lesson.IDCourse
                        join attachment in _attachmentRepository on lesson.Id equals attachment.IDTable
                        where lesson.Id == id
                        select new { lesson, course, attachment };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Lesson), id);
            }

            var lessonDto = ObjectMapper.Map<Lesson, LessonDto>(queryResult.lesson);
            lessonDto.CoursesName = queryResult.course.Name;
            lessonDto.URL = queryResult.attachment.URL;
            lessonDto.Benefit = queryResult.course.Benefit;
            return lessonDto;
        }

        public async Task<ListResultDto<CourseLookupDto>> GetCourseLookupAsync()
        {
            var course = await _courseRepository.GetListAsync();

            return new ListResultDto<CourseLookupDto>(
                ObjectMapper.Map<List<Course>, List<CourseLookupDto>>(course));
        }

        public async Task<PagedResultDto<LessonDto>> GetListAsync(GetLessonListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Lesson.Name);
            }

            //Get the IQueryable<Book> from the repository
            var queryable = await _courseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from course in queryable
                        join lesson in _lessonRepository on course.Id equals lesson.IDCourse
                        join attachment in _attachmentRepository on lesson.Id equals attachment.IDTable
                        orderby input.Sorting
                        select new { lesson, course, attachment };
            var totalCount = await _lessonRepository.GetCountAsync();
            //Paging
            query = query
                .Skip(input.SkipCount)
                .Take((int)totalCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var lessonDtos = queryResult.Select(x =>
            {
                var lessonDto = ObjectMapper.Map<Lesson, LessonDto>(x.lesson);
                lessonDto.CoursesName = x.course.Name;
                lessonDto.URL = x.attachment.URL;
                lessonDto.Benefit = x.course.Benefit;
                return lessonDto;
            }).ToList();

            //Get the total count with another query


            return new PagedResultDto<LessonDto>(
                totalCount,
                lessonDtos
            );
        }
        public async Task<PagedResultDto<LessonDto>> GetListAsyncByCourseID(Guid ID)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await _courseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from course in queryable
                        join lesson in _lessonRepository on course.Id equals lesson.IDCourse
                        join attachment in _attachmentRepository on lesson.Id equals attachment.IDTable
                        where course.Id == ID
                        select new { lesson, course, attachment };

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var lessonDtos = queryResult.Select(x =>
            {
                var lessonDto = ObjectMapper.Map<Lesson, LessonDto>(x.lesson);
                lessonDto.CoursesName = x.course.Name;
                lessonDto.URL = x.attachment.URL;
                lessonDto.Benefit = x.course.Benefit;
                return lessonDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = _lessonRepository.Count(x => x.IDCourse == ID);

            return new PagedResultDto<LessonDto>(
                totalCount,
                lessonDtos
            );
        }
        //[Authorize(ProjectPermissions.Lessons.Edit)]
        public async Task UpdateAsync(Guid id, UpdateLessonDto input)
        {
            var lesson = await _lessonRepository.GetAsync(id);
            var att = await _attachmentRepository.FindAsync(x => x.IDTable == id);

            if (lesson.Name != input.Name)
            {
                await _lessonManager.ChangeNameAsync(lesson, input.Name);
            }

            lesson.Description = input.Description;
            lesson.IDCourse = input.IDCourse;
            att.URL = input.Url;

            await _lessonRepository.UpdateAsync(lesson);
            await _attachmentRepository.UpdateAsync(att);
        }
    }
}