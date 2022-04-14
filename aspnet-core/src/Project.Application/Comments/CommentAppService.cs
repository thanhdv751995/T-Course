using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Project.Attachments;
using Project.BaiHocs;
using Project.DanhMucs;
using Project.KhoaHocs;
using Project.NguoiDungs;
using Project.Permissions;
using Project.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;


namespace Project.Comments
{
    [Authorize]
    public class CommentAppService : ProjectAppService, ICommentAppService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly CommentManager _commentManager;
        private readonly IRepository<Lesson, Guid> _lessonRepository;
        private readonly IRepository<Attachment, Guid> _attachmentRepository;
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly IConfiguration _configuration;

        public CommentAppService(

            IConfiguration configuration,
           ICommentRepository commentRepository,
           CommentManager commentManager,
           IRepository<Attachment, Guid> attachmentRepository,
        IRepository<AppUser, Guid> userRepository,
            IRepository<Lesson, Guid> lessonRepository)

        {
            _configuration = configuration;
            _attachmentRepository = attachmentRepository;
            _commentRepository = commentRepository;
            _commentManager = commentManager;
            _lessonRepository = lessonRepository;
            _userRepository = userRepository;
        }
        public async Task<CommentDto> CreateAsync(CreateCommentDto input)
        {
            var comment = await _commentManager.CreateAsync(
                input.content,
                input.IDLesson,
                input.IDParent,
                input.IDUser
            );

            await _commentRepository.InsertAsync(comment);

            return ObjectMapper.Map<Comment, CommentDto>(comment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var queryable = _commentRepository.Where(l => l.IDParent == id).ToArray();
            if (queryable.Length > 0)
            {
                for (int i = 0; i < queryable.Length; i++)
                {
                    await _commentRepository.DeleteAsync(queryable[i].Id);
                }
            }
            await _commentRepository.DeleteAsync(id);

        }
        [AllowAnonymous]
        public async Task<CommentDto> GetAsync(Guid id)
        {
            var queryable = await _commentRepository.GetQueryableAsync();
            var query = from Lesson in _lessonRepository
                        join comment in queryable on Lesson.Id equals comment.IDLesson
                        join user in _userRepository on comment.IDUser equals user.Id
                        where comment.Id == id
                        select new { Lesson, comment, user };
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Comment), id);
            }
            var commentDto = ObjectMapper.Map<Comment, CommentDto>(queryResult.comment);
            commentDto.LessonName = queryResult.Lesson.Name;
            commentDto.UserName = queryResult.user.Name;
            return commentDto;
        }
        [AllowAnonymous]
        public async Task<ListResultDto<LessonLookupDto>> GetLessonLookupAsync()
        {
            var lessons = await _lessonRepository.GetListAsync();

            return new ListResultDto<LessonLookupDto>(
                ObjectMapper.Map<List<Lesson>, List<LessonLookupDto>>(lessons)
            );
        }
        [AllowAnonymous]
        public async Task<PagedResultDto<CommentDto>> GetListAsync(GetCommentListDto input)
        {
            var queryable = await _commentRepository.GetQueryableAsync();
            var query = from lesson in _lessonRepository
                        join comment in queryable on lesson.Id equals comment.IDLesson
                        join user in _userRepository on comment.IDUser equals user.Id
                        orderby input.Sorting //TODO: Can not sort like that!
                        select new { lesson, comment, user };
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var commentDtos = queryResult.Select(x =>
            {
                var commentDto = ObjectMapper.Map<Comment, CommentDto>(x.comment);
                commentDto.UserName = x.user.Name;
                commentDto.LessonName = x.lesson.Name;
                return commentDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _commentRepository.GetCountAsync();

            return new PagedResultDto<CommentDto>(
                totalCount,
                commentDtos
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
        public async Task UpdateAsync(Guid id, UpdateCommentDto input)
        {

            var comment = await _commentRepository.GetAsync(id);
            if (comment.content != input.content)
            {
                await _commentManager.ChangeNameAsync(comment, input.content);
            }
            comment.IDParent = input.IDParent;
            comment.IDLesson = input.IDLesson;
            comment.IDUser = input.IDUser;


            await _commentRepository.UpdateAsync(comment);
        }
        [AllowAnonymous]
        public async Task<ListResultDto<CommentDto>> GetListCommentByIDlesson(GetCommentListDto input, Guid ID)
        {
            var queryable = await _commentRepository.GetQueryableAsync();

            var query = from lesson in _lessonRepository
                        join comment in queryable on lesson.Id equals comment.IDLesson
                        join user in _userRepository on comment.IDUser equals user.Id
                        join attachment in _attachmentRepository on comment.Id equals attachment.IDTable into ps
                        from p in ps.DefaultIfEmpty()
                        where comment.IDLesson == ID && !user.IsDeleted
                        orderby comment.IDParent ascending, comment.CreationTime descending
                        select new { lesson, comment, user, Url = p == null ? null : p.URL };
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var commentDtos = queryResult.Select(x =>
            {
                var commentDto = ObjectMapper.Map<Comment, CommentDto>(x.comment);
                commentDto.UserName = x.user.Name;
                commentDto.LessonName = x.lesson.Name;
                commentDto.CreationTime = x.comment.CreationTime;
                commentDto.Avartar = x.user.Avatar;
                commentDto.Url = x.Url;
                return commentDto;
            }).ToList();

            var totalCount = _commentRepository.Count(x => x.IDLesson == ID);

            return new PagedResultDto<CommentDto>(
                totalCount,
                commentDtos
            );
        }
        [AllowAnonymous]
        public async Task<ListResultDto<CommentDto>> GetListIDChildByIDPrent(Guid ID)
        {
            var queryable = await _commentRepository.GetQueryableAsync();

            var query = from lesson in _lessonRepository
                        join comment in queryable on lesson.Id equals comment.IDLesson
                        join user in _userRepository on comment.IDUser equals user.Id
                        join attachment in _attachmentRepository on comment.Id equals attachment.IDTable into ps
                        from p in ps.DefaultIfEmpty()
                        where comment.IDParent == ID
                        select new { lesson, comment, user, Url = p == null ? null : p.URL };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var commentDtos = queryResult.Select(x =>
            {
                var commentDto = ObjectMapper.Map<Comment, CommentDto>(x.comment);
                commentDto.Id = x.comment.Id;
                commentDto.Avartar = x.user.Avatar;
                commentDto.Url = x.Url;
                return commentDto;
            }).ToList();

            var totalCount = _commentRepository.Count(x => x.IDParent == ID);

            return new PagedResultDto<CommentDto>(
                totalCount,
                commentDtos
            );
        }
        [AllowAnonymous]
        public int GetCount()
        {
            return _commentRepository.Count();
        }
    }
}
