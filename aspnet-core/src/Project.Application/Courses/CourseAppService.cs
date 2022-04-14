using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Project.Attachments;
using Project.BaiHocs;
using Project.Courses;
using Project.DanhMucs;
using Project.Lessons;
using Project.Localization;
using Project.Permissions;
using Project.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic;
using Project.DanhGias;
using Project.Rates;
using Project.ThamGiaKhoaHocs;
using Project.JoinCouses;
namespace Project.KhoaHocs

{
    //[RemoteService(IsEnabled = false)]
    //[Authorize]
    public class CourseAppService : ProjectAppService, ICourseAppService,IRepositoryShare<CourseDto,Guid>,IRepositoryCreateUpdateShare<CreateCourseDto,Guid>,IGetListShareFilter<CourseDto,GetCourseListDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly CourseManager _courseManager;
        private readonly AttachmentManager _attManager;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly IRepository<Attachment, Guid> _attachmentRepository;
        private readonly IRepository<Lesson, Guid> _lessonRepository;
        private readonly IRepository<Rate, Guid> _rateRepository;
        private readonly IRepository<JoinCourse, Guid> _joinRepository;

        //private readonly IStringLocalizer<ProjectResource> _localizer;
        public CourseAppService(
            ICourseRepository courseRepository,
            CourseManager courseManager,
            IRepository<Category, Guid> categoryRepository,
            IRepository<AppUser, Guid> userRepository,
            IAttachmentRepository attachmentRepository,
            ILessonRepository lessonRepository,
            IRateRepository rateRepository,
            IJoinCourseRepository joinCourseRepository,
            AttachmentManager attManager
            //IStringLocalizer<ProjectResource> stringLocalizer
            )
        {
            _courseRepository = courseRepository;
            _courseManager = courseManager;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _attachmentRepository = attachmentRepository;
            _lessonRepository = lessonRepository;
            _rateRepository = rateRepository;
            _joinRepository = joinCourseRepository;
            //_localizer = stringLocalizer;
            _attManager = attManager;
        }
        [AllowAnonymous]
        public async Task<CourseDto> GetAsync(Guid id)
        {
            var queryable = await _courseRepository.GetQueryableAsync();

            var query = from category in _categoryRepository
                        join course in queryable on category.Id equals course.IDCategory
                        join attachment in _attachmentRepository.DefaultIfEmpty() on course.Id equals attachment.IDTable
                        join user in _userRepository on course.IDUser equals user.Id
                        where course.Id == id
                        orderby course.CreationTime descending
                        select new { category, course, user, attachment };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Course), id);
            }

            var courseDto = ObjectMapper.Map<Course, CourseDto>(queryResult.course);
            courseDto.CategoryName = queryResult.category.Name;
            courseDto.NameUser = queryResult.user.Name;
            courseDto.URL = queryResult.attachment.URL;
            courseDto.RateCount = _rateRepository.Count(r => r.IDCourse == id);
            courseDto.RateTotalPoint = _rateRepository.Where(r => r.IDCourse == id).Sum(rate => rate.RatePoint);
            _ = courseDto.RateCount == 0 ? courseDto.RateAverage = 0
            : courseDto.RateAverage = Math.Round(courseDto.RateTotalPoint / courseDto.RateCount, 1);
            courseDto.CountStudent = _joinRepository.Count(j => j.IDCourse == courseDto.Id);
            return courseDto;
        }
        //[AllowAnonymous]
        public async Task<PagedResultDto<CourseDto>> GetListAsync(GetCourseListDto input)
        {
            
            if (input.Filter.IsNullOrWhiteSpace())
            {
                input.Filter = "";
            }
            var queryable = await _courseRepository.GetQueryableAsync();

            var query = from category in _categoryRepository
                        join course in queryable on category.Id equals course.IDCategory
                        join attachment in _attachmentRepository.DefaultIfEmpty() on course.Id equals attachment.IDTable
                        join user in _userRepository on course.IDUser equals user.Id
                        where course.Name.Contains(input.Filter)
                        orderby course.CreationTime descending
                        select new { category, course, user, attachment };
            var totalCount = input.Filter == null
                ? await _courseRepository.CountAsync()
                : await _courseRepository.CountAsync(
                    course => course.Name.Contains(input.Filter));
            if (input.MaxResultCount>0&&input.MaxResultCount!=10)
            {
                query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            }
            else query = query.Skip(input.SkipCount).Take(totalCount);
            var queryResult = await AsyncExecuter.ToListAsync(query);

            var courseDtos = queryResult.Select(x =>
            {
                var courseDto = ObjectMapper.Map<Course, CourseDto>(x.course);
                courseDto.CategoryName = x.category.Name;
                courseDto.NameUser = x.user.Name;
                courseDto.URL = x.attachment.URL ?? null;
                courseDto.RateCount = _rateRepository.Count(r => r.IDCourse == courseDto.Id);
                courseDto.RateTotalPoint = _rateRepository.Where(r => r.IDCourse == courseDto.Id).Sum(rate => rate.RatePoint);
                _ = courseDto.RateCount == 0 ? courseDto.RateAverage = 0
                : courseDto.RateAverage = Math.Round(courseDto.RateTotalPoint / courseDto.RateCount, 1);
                courseDto.CountStudent = _joinRepository.Count(j => j.IDCourse == courseDto.Id);
                courseDto.CreationTime = x.course.CreationTime;
                return courseDto;
            }).ToList();



            return new PagedResultDto<CourseDto>(
                totalCount,
                courseDtos
            );
        }

        public async Task<CourseDto> CreateAsync(CreateCourseDto input)
        {
            var course = await _courseManager.CreateAsync(
                input.Name,
                input.Description,
                input.Benefit,
                input.IDCategory,
                input.IDUser
            );

            await _courseRepository.InsertAsync(course);

            var att = await _attManager.CreateAsync(
                 input.URL,
                 course.Id
    ); ;

            await _attachmentRepository.InsertAsync(att);
            return ObjectMapper.Map<Course, CourseDto>(course);
        }

        public async Task UpdateAsync(Guid id, UpdateCourseDto input)
        {
            var course = await _courseRepository.GetAsync(id);
            var att = await _attachmentRepository.FindAsync(x => x.IDTable == id);
            if (course.Name != input.Name)
            {
                await _courseManager.ChangeNameAsync(course, input.Name);
            }

            course.Name = input.Name;
            course.Description = input.Description;
            course.Benefit = input.Benefit;
            course.IDCategory = input.IDCategory;
            course.IDUser = input.IDUser;
            att.URL = input.Url;
            await _courseRepository.UpdateAsync(course);
            await _attachmentRepository.UpdateAsync(att);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _courseRepository.DeleteAsync(id);
            var less = _lessonRepository.Where(l => l.IDCourse == id).ToArray();
            var att = _attachmentRepository.Where(l => l.IDTable == id).ToArray();
            if (att.Length > 0)
            {
                for (int i = 0; i < att.Length; i++)
                {
                    await _attachmentRepository.DeleteAsync(att[i].Id);
                }
            }
            if (less.Length > 0)
            {
                for (int i = 0; i < less.Length; i++)
                {
                    await _lessonRepository.DeleteAsync(less[i].Id);
                }
            }
        }

        [AllowAnonymous]
        public async Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync()
        {
            var categories = await _categoryRepository.GetListAsync();

            return new ListResultDto<CategoryLookupDto>(
                ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories)
            );
        }

        [AllowAnonymous]
        public async Task<ListResultDto<UserLookupDto>> GetUserLookupAsync()
        {
            var users = await _userRepository.GetListAsync();

            return new ListResultDto<UserLookupDto>(
                ObjectMapper.Map<List<AppUser>, List<UserLookupDto>>(users)
            );
        }

        [AllowAnonymous]
        public async Task<ListResultDto<AttachmentLookupDto>> GetAttachmentLookupAsync()
        {
            var attachment = await _attachmentRepository.GetListAsync();

            return new ListResultDto<AttachmentLookupDto>(
                ObjectMapper.Map<List<Attachment>, List<AttachmentLookupDto>>(attachment)
            );
        }

        [AllowAnonymous]
        public async Task<ListResultDto<CourseDto>> GetListLessonByIDCourse(Guid ID)
        {
            var queryable = await _courseRepository.GetQueryableAsync();

            var query = from user in _userRepository
                        join course in queryable on user.Id equals course.IDUser
                        join category in _categoryRepository on course.IDCategory equals category.Id
                        join lesson in _lessonRepository on course.Id equals lesson.IDCourse
                        join attachment in _attachmentRepository on lesson.Id equals attachment.IDTable
                        where course.Id == ID
                        select new { course, lesson, attachment, user, category };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var courseDtos = queryResult.Select(x =>
            {
                var courseDto = ObjectMapper.Map<Course, CourseDto>(x.course);
                courseDto.LessonName = x.lesson.Name;
                courseDto.LessonDescription = x.lesson.Description;
                courseDto.LessonURL = x.attachment.URL;
                courseDto.LessonID = x.lesson.Id;
                courseDto.NameUser = x.user.Name;
                courseDto.CategoryName = x.category.Name;

                return courseDto;
            }).ToList();

            var totalCount = _lessonRepository.Count(x => x.IDCourse == ID);

            return new PagedResultDto<CourseDto>(
                totalCount,
                courseDtos
            );
        }


        [AllowAnonymous]
        public async Task<ListResultDto<CourseDto>> GetListCourseByCategoryID(Guid ID)
        {
            var queryable = await _courseRepository.GetQueryableAsync();

            var query = from category in _categoryRepository
                        join course in queryable on category.Id equals course.IDCategory
                        join attachment in _attachmentRepository on course.Id equals attachment.IDTable
                        join user in _userRepository on course.IDUser equals user.Id
                        where course.IDCategory == ID
                        select new { category, course, user, attachment };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var courseDtos = queryResult.Select(x =>
            {
                var courseDto = ObjectMapper.Map<Course, CourseDto>(x.course);
                courseDto.Name = x.course.Name;
                courseDto.Description = x.course.Description;
                courseDto.Benefit = x.course.Benefit;
                courseDto.IDCategory = x.category.Id;
                courseDto.IDUser = x.user.Id;
                courseDto.URL = x.attachment.URL;
                courseDto.CategoryName = x.category.Name;
                courseDto.NameUser = x.user.Name;
                courseDto.URL = x.attachment.URL;
                courseDto.CountStudent = _joinRepository.Count(j => j.IDCourse == courseDto.Id);
                return courseDto;
            }).ToList();

            var totalCount = _courseRepository.Count(x => x.IDCategory == ID);

            return new PagedResultDto<CourseDto>(
                totalCount,
                courseDtos
                );
        }

        [AllowAnonymous]
        public async Task<CourseDto> GetByIdAsync(Guid id)
        {
            var queryable = await _courseRepository.GetQueryableAsync();
            //Prepare a query to join books and authors
            var query = from category in _categoryRepository
                        join course in queryable on category.Id equals course.IDCategory
                        join user in _userRepository on course.IDUser equals user.Id
                        where course.Id == id
                        select new { category, course, user };
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Course), id);
            }
            var courseDto = ObjectMapper.Map<Course, CourseDto>(queryResult.course);
            courseDto.CategoryName = queryResult.category.Name;
            courseDto.NameUser = queryResult.user.Name;
            return courseDto;
            //var nguoidung = await _userRepository.GetAsync(id);
            //return ObjectMapper.Map<NguoiDung, NguoiDungDto>(nguoidung);
        }

        [AllowAnonymous]
        public async Task<PagedResultDto<CourseDto>> GetListAllAsync(GetCourseListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Course.Name);
            }
            //Get the IQueryable<Book> from the repository
            var queryable = await _courseRepository.GetQueryableAsync();
            //Prepare a query to join books and authors
            var query = from category in _categoryRepository
                        join course in queryable on category.Id equals course.IDCategory
                        join user in _userRepository on course.IDUser equals user.Id
                        orderby input.Sorting //TODO: Can not sort like that!
                        select new { category, course, user };
            //Paging
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);
            //Convert the query result to a list of BookDto objects
            var courseDtos = queryResult.Select(x =>
            {
                var courseDto = ObjectMapper.Map<Course, CourseDto>(x.course);
                courseDto.CategoryName = x.category.Name;
                courseDto.NameUser = x.user.Name;
                return courseDto;
            }).ToList();
            //Get the total count with another query
            var totalCount = await _courseRepository.GetCountAsync();
            return new PagedResultDto<CourseDto>(
                totalCount,
                courseDtos
            );
        }


        [AllowAnonymous]
        public IQueryable<Category> GetListRelative(Guid idUser)
        {
            var lastCourse = _joinRepository.Where(x => x.IDUser == idUser).ToArray().OrderByDescending(x => x.CreationTime).FirstOrDefault();

            var categoryCourse = _courseRepository.Where(c => c.Id == lastCourse.IDCourse).ToArray().FirstOrDefault();

            var query = _categoryRepository.Where(c => c.Id == categoryCourse.IDCategory);

            return query;
        }

        public async Task<CourseDto> GetByIDCategoryAsync(Guid id)
        {
            var queryable = await _courseRepository.GetQueryableAsync();

            var query = from category in _categoryRepository
                        join course in queryable on category.Id equals course.IDCategory
                        join attachment in _attachmentRepository.DefaultIfEmpty() on course.Id equals attachment.IDTable
                        join user in _userRepository on course.IDUser equals user.Id
                        where course.Id == id
                        orderby course.CreationTime descending
                        select new { category, course, user, attachment };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Course), id);
            }

            var courseDto = ObjectMapper.Map<Course, CourseDto>(queryResult.course);
            courseDto.CategoryName = queryResult.category.Name;
            courseDto.NameUser = queryResult.user.Name;
            courseDto.URL = queryResult.attachment.URL;
            courseDto.RateCount = _rateRepository.Count(r => r.IDCourse == id);
            courseDto.RateTotalPoint = _rateRepository.Where(r => r.IDCourse == id).Sum(rate => rate.RatePoint);
            _ = courseDto.RateCount == 0 ? courseDto.RateAverage = 0
            : courseDto.RateAverage = Math.Round(courseDto.RateTotalPoint / courseDto.RateCount, 1);
            courseDto.CountStudent = _joinRepository.Count(j => j.IDCourse == courseDto.Id);
            return courseDto;
        }

        public async Task DeleteCategoryByIDAsync(Guid id)
        {
            await _courseRepository.DeleteAsync(id);
            var less = _lessonRepository.Where(l => l.IDCourse == id).ToArray();
            var att = _attachmentRepository.Where(l => l.IDTable == id).ToArray();
            if (att.Length > 0)
            {
                for (int i = 0; i < att.Length; i++)
                {
                    await _attachmentRepository.DeleteAsync(att[i].Id);
                }
            }
            if (less.Length > 0)
            {
                for (int i = 0; i < less.Length; i++)
                {
                    await _lessonRepository.DeleteAsync(less[i].Id);
                }
            }
        }

        public async Task UpdateCategoryAsync(CreateCourseDto input, Guid id)
        {
            var course = await _courseRepository.GetAsync(id);
            var att = await _attachmentRepository.FindAsync(x => x.IDTable == id);
            if (course.Name != input.Name)
            {
                await _courseManager.ChangeNameAsync(course, input.Name);
            }

            course.Name = input.Name;
            course.Description = input.Description;
            course.Benefit = input.Benefit;
            course.IDCategory = input.IDCategory;
            course.IDUser = input.IDUser;
            att.URL = input.URL;
            await _courseRepository.UpdateAsync(course);
            await _attachmentRepository.UpdateAsync(att);
        }

        public async Task PostInsertCategoryAsync(CreateCourseDto input)
        {
            var course = await _courseManager.CreateAsync(
                input.Name,
                input.Description,
                input.Benefit,
                input.IDCategory,
                input.IDUser
            );

            await _courseRepository.InsertAsync(course);

            var att = await _attManager.CreateAsync(
                 input.URL,
                 course.Id
    ); ;

            await _attachmentRepository.InsertAsync(att);

        }

        public async Task<PagedResultDto<CourseDto>> GetAllListCategoryAsync(GetCourseListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Course.Name);
            }
            if (input.Filter.IsNullOrWhiteSpace())
            {
                input.Filter = "";
            }
            var queryable = await _courseRepository.GetQueryableAsync();

            var query = from category in _categoryRepository
                        join course in queryable on category.Id equals course.IDCategory
                        join attachment in _attachmentRepository.DefaultIfEmpty() on course.Id equals attachment.IDTable
                        join user in _userRepository on course.IDUser equals user.Id
                        where course.Name.Contains(input.Filter)
                        orderby course.CreationTime descending
                        select new { category, course, user, attachment };
            var totalCount = input.Filter == null
                ? await _courseRepository.CountAsync()
                : await _courseRepository.CountAsync(
                    course => course.Name.Contains(input.Filter));
            if (input.MaxResultCount > 0)
            {
                query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            }
            else query = query.Skip(input.SkipCount).Take(totalCount);
            var queryResult = await AsyncExecuter.ToListAsync(query);

            var courseDtos = queryResult.Select(x =>
            {
                var courseDto = ObjectMapper.Map<Course, CourseDto>(x.course);
                courseDto.CategoryName = x.category.Name;
                courseDto.NameUser = x.user.Name;
                courseDto.URL = x.attachment.URL ?? null;
                courseDto.RateCount = _rateRepository.Count(r => r.IDCourse == courseDto.Id);
                courseDto.RateTotalPoint = _rateRepository.Where(r => r.IDCourse == courseDto.Id).Sum(rate => rate.RatePoint);
                _ = courseDto.RateCount == 0 ? courseDto.RateAverage = 0
                : courseDto.RateAverage = Math.Round(courseDto.RateTotalPoint / courseDto.RateCount, 1);
                courseDto.CountStudent = _joinRepository.Count(j => j.IDCourse == courseDto.Id);
                courseDto.CreationTime = x.course.CreationTime;
                return courseDto;
            }).ToList();



            return new PagedResultDto<CourseDto>(
                totalCount,
                courseDtos
            );
        }

        public async Task<bool> GetCheckPhoneNumberExistAsync(string PhoneNumber,Guid ID)
        {
            var exist = await _userRepository.FindAsync(x => x.PhoneNumber==PhoneNumber&&x.Id!= ID);
            return (exist != null) ? true : false;
        }
    }
}