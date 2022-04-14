using Project.Attachments;
using Project.JoinCouses;
using Project.KhoaHocs;
using Project.Rates;
using Project.ThamGiaKhoaHocs;
using Project.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Project.JoinCourses
{
    public class JoinCourseAppService : ProjectAppService, IJoinCourseAppService
    {
        private readonly IJoinCourseRepository _joincourseRepository;
        private readonly JoinCourseManager _joincourseManager;
        private readonly IRepository<Course, Guid> _courseRepository;
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly IRepository<Attachment, Guid> _attachmentsRepository;
        public JoinCourseAppService(
            IJoinCourseRepository joincourseRepository,
            JoinCourseManager joincourseManager,
            IRepository<Course, Guid> courseRepository,
            IRepository<AppUser, Guid> userRepository,
            IRepository<Attachment,Guid> attachmentRepository
            )
        {
            _joincourseRepository = joincourseRepository;
            _joincourseManager = joincourseManager;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _attachmentsRepository = attachmentRepository;
        }
        public async Task<JoinCourseDto> CreateAsync(CreateJoinCourseDto input)
        {
            var joincourse = await _joincourseManager.CreateAsync(
                input.IDCourse,
                input.IDUser,
                false

            );

            await _joincourseRepository.InsertAsync(joincourse);

            return ObjectMapper.Map<JoinCourse, JoinCourseDto>(joincourse);
        }

        public async Task<JoinCourseDto> GetAsync(Guid id)
        {
            var queryable = await _joincourseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from course in _courseRepository
                        join joincourse in queryable on course.Id equals joincourse.IDCourse
                        join user in _userRepository on joincourse.IDUser equals user.Id
                        where joincourse.Id == id
                        select new { joincourse, course, user };


            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Course), id);
            }

            var joincourseDto = ObjectMapper.Map<JoinCourse, JoinCourseDto>(queryResult.joincourse);
            joincourseDto.UserName = queryResult.user.Name;
            joincourseDto.CoursesName = queryResult.course.Name;
            return joincourseDto;
        }
        public Task<bool> GetCheckEverJoinCourseAsync(Guid idUser)
        {
            int exist = _joincourseRepository.Count(x => x.IDUser == idUser);
            return Task.FromResult((exist > 0) ? true : false);
        }

        public async Task<ListResultDto<CourseLookupDto>> GetCourseLookupAsync()
        {
            var course = await _courseRepository.GetListAsync();
            
            return new ListResultDto<CourseLookupDto>(
                ObjectMapper.Map<List<Course>, List<CourseLookupDto>>(course)
            );
        }

        public async Task<bool> GetJoinCourseAsync(Guid idUser, Guid idCourse)
        {
            var exist = await _joincourseRepository.FindAsync(x => x.IDUser == idUser && x.IDCourse == idCourse);
            return (exist != null) ? true : false;
        }
        public async Task<bool> GetCheckHasBeenLockJoinCourseAsync(Guid idUser, Guid idCourse)
        {
            var exist = await _joincourseRepository.FindAsync(x => x.IDUser == idUser && x.IDCourse == idCourse&&x.HasBeenLock==true);
            return (exist != null) ? true : false;
        }
        public async Task<PagedResultDto<JoinCourseDto>> GetListUserJoinCourseAsync(Guid IDCourse)
        {

            //Get the IQueryable<Book> from the repository
            var queryable = await _joincourseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from course in _courseRepository
                        join joincourse in queryable on course.Id equals joincourse.IDCourse
                        join user in _userRepository on joincourse.IDUser equals user.Id
                        where joincourse.IDCourse==IDCourse
                        select new { joincourse, course, user };
            var totalCount = _joincourseRepository.Where(x => x.IDCourse == IDCourse).Count();
            query = query.Take(totalCount);
            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var joincourseDtos = queryResult.Select(x =>
            {
                var joincourseDto = ObjectMapper.Map<JoinCourse, JoinCourseDto>(x.joincourse);
                joincourseDto.CoursesName = x.course.Name;
                joincourseDto.UserName = x.user.Name;
                joincourseDto.CreationTime = x.joincourse.CreationTime;
                return joincourseDto;
            }).ToList();

            //Get the total count with another query
            

            return new PagedResultDto<JoinCourseDto>(
                totalCount,
                joincourseDtos
            );
        }
        public async Task DeleteAsync(Guid id)
        {
            await _joincourseRepository.DeleteAsync(id);
        }
        public async Task<PagedResultDto<JoinCourseDto>> GetListCourseUserJoinAsync(Guid IDUser)
        {

            //Get the IQueryable<Book> from the repository
            var queryable = await _joincourseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from course in _courseRepository
                        join joincourse in queryable on course.Id equals joincourse.IDCourse
                        join user in _userRepository on joincourse.IDUser equals user.Id
                        join attachment in _attachmentsRepository on course.Id equals attachment.IDTable into temp
                        from p in temp.DefaultIfEmpty()
                        where joincourse.IDUser == IDUser
                        select new { joincourse, course, user,Url=p==null?null:p.URL };

            var totalCount = _joincourseRepository.Where(x => x.IDUser == IDUser).Count();
            query = query.Take(totalCount);
            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var joincourseDtos = queryResult.Select(x =>
            {
                var joincourseDto = ObjectMapper.Map<JoinCourse, JoinCourseDto>(x.joincourse);
                joincourseDto.CoursesName = x.course.Name;
                joincourseDto.UserName = x.user.Name;
                joincourseDto.CreationTime = x.joincourse.CreationTime;
                joincourseDto.URL = x.Url;
                return joincourseDto;
            }).ToList();

            //Get the total count with another query
           

            return new PagedResultDto<JoinCourseDto>(
                totalCount,
                joincourseDtos
            );
        }

        public async Task<ListResultDto<UserLookupDto>> GetUserLookupAsync()
        {
            var users = await _userRepository.GetListAsync();

            return new ListResultDto<UserLookupDto>(
                ObjectMapper.Map<List<AppUser>, List<UserLookupDto>>(users)
            );
        }

        public async Task UpdateAsync(Guid id)
        {
            var joincourse = await _joincourseRepository.GetAsync(id);

            joincourse.HasBeenLock = !joincourse.HasBeenLock;
            await _joincourseRepository.UpdateAsync(joincourse);
        }
    }
}
