using Project.DanhGias;
using Project.KhoaHocs;
using Project.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Project.Rates
{
    public class RateAppService : ProjectAppService, IRateAppService
    {
        private readonly IRateRepository _rateRepository;
        private readonly RateManager _rateManager;

        //private readonly IroleRepository _roleRepository;
        private readonly IRepository<AppUser, Guid> _userRepository;

        private readonly IRepository<Course, Guid> _courseRepository;

        public RateAppService(
            IRateRepository rateRepository,
            RateManager rateManager,
            IRepository<AppUser, Guid> userRepository,
            IRepository<Course, Guid> courseRepository)
        {
            _rateRepository = rateRepository;
            _rateManager = rateManager;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        public async Task<RateDto> CreateAsync(CreateRateDto input)
        {

            DateTime localDate = DateTime.Now;
            var queryable = await _courseRepository.GetQueryableAsync();

            var query = from user in _userRepository
                        where input.IDUser == user.Id
                        select new {user};
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            var rate = await _rateManager.CreateAsync(
                input.RatePoint,
                input.Content,
                input.IDCourse,
                input.IDUser
                );
            await _rateRepository.InsertAsync(rate);

            var rateDto= ObjectMapper.Map<Rate, RateDto>(rate);
            rateDto.UserName = queryResult.user.Name;
            rateDto.CreationTime = localDate;
            return rateDto;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _rateRepository.DeleteAsync(id);
        }

        public async Task<RateDto> GetAsync(Guid id)
        {
            var queryable = await _rateRepository.GetQueryableAsync();
            var query = from course in _courseRepository
                        join rate in queryable on course.Id equals rate.IDCourse
                        join user in _userRepository on rate.IDUser equals user.Id
                        where rate.Id == id
                        select new { course, rate, user };
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Rate), id);
            }
            var rateDto = ObjectMapper.Map<Rate, RateDto>(queryResult.rate);
            
            rateDto.CourseName = queryResult.course.Name;
            rateDto.UserName = queryResult.user.Name;
            return rateDto;
        }

        public async Task<ListResultDto<CourseLookupDto>> GetCourseLookupAsync()
        {
            var courses = await _courseRepository.GetListAsync();

            return new ListResultDto<CourseLookupDto>(
                ObjectMapper.Map<List<Course>, List<CourseLookupDto>>(courses)
            );
        }

        public async Task<ListResultDto<RateDto>> GetListRateByIDCourse(Guid ID, GetRateListDto input)
        {
            var queryable = await _courseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from course in queryable
                        join rate in _rateRepository on course.Id equals rate.IDCourse
                        join user in _userRepository on rate.IDUser equals user.Id
                        where course.Id == ID
                        orderby rate.CreationTime descending
                        select new { course, rate, user };
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var rateDtos = queryResult.Select(x =>
            {
                var rateDto = ObjectMapper.Map<Rate, RateDto>(x.rate);
                rateDto.RateTotalPoint = _rateRepository.Where(r => r.IDCourse == ID).Sum(rate => rate.RatePoint);
                rateDto.RateCount = _rateRepository.Count(r => r.IDCourse == ID);
                _ = rateDto.RateCount == 0 ? rateDto.RateAverage = 0
                : rateDto.RateAverage = Math.Round(rateDto.RateTotalPoint / rateDto.RateCount, 1);
                rateDto.CourseName = x.course.Name;
                rateDto.UserName = x.user.Name;
                rateDto.Avarta = x.user.Avatar;
                return rateDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = _rateRepository.Count(x => x.IDCourse == ID);

            return new PagedResultDto<RateDto>(
                totalCount,
                rateDtos
            );
        }
        public async Task<PagedResultDto<RateDto>> GetListAsync(GetRateListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Rate.Content);
            }
            var queryable = await _rateRepository.GetQueryableAsync();
            var query = from course in _courseRepository
                        join rate in queryable on course.Id equals rate.IDCourse
                        join user in _userRepository on rate.IDUser equals user.Id
                        orderby input.Sorting //TODO: Can not sort like that!
                        select new { course, rate, user };
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var rateDtos = queryResult.Select(x =>
            {
                var rateDto = ObjectMapper.Map<Rate, RateDto>(x.rate);
                rateDto.UserName = x.user.Name;
                rateDto.CourseName = x.course.Name;

                return rateDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _rateRepository.GetCountAsync();

            return new PagedResultDto<RateDto>(
                totalCount,
                rateDtos
            );
        }

        public async Task<ListResultDto<UserLookupDto>> GetUserLookupAsync()
        {
            var users = await _userRepository.GetListAsync();

            return new ListResultDto<UserLookupDto>(
                ObjectMapper.Map<List<AppUser>, List<UserLookupDto>>(users)
            );
        }
        public async Task UpdateAsync(Guid id, UpdateRateDto input)
        {
            var rate = await _rateRepository.GetAsync(id);

            rate.RatePoint = input.RatePoint;
            rate.Content = input.Content;
            rate.IDUser = input.IDUser;
            rate.IDCourse = input.IDCourse;

            await _rateRepository.UpdateAsync(rate);
        }

        public int GetCount()
        {
            return _rateRepository.Count(x=>x.RatePoint>3);
        }
        public int GetUserCount()
        {
            return _userRepository.Count();
        }
        public async Task<bool> GetExistRate(Guid IdCourse, Guid IdUser)
        {
            var existingUserRate = await _rateRepository.FindByIDUserAsync(IdUser, IdCourse);
            return (existingUserRate != null) ? true : false;
        }
    }
}