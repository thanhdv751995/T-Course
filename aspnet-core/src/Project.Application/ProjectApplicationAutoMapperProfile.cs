using AutoMapper;
using Project.Attachments;
using Project.BaiHocs;
using Project.Comments;
using Project.Contact;
using Project.Courses;
using Project.DanhGias;
using Project.DanhMucs;
using Project.DuLieu;
using Project.JoinCourses;
using Project.KhoaHocs;
using Project.Lessons;
using Project.NguoiDungs;
using Project.PhanQuyens;
using Project.Rates;
using Project.ThamGiaKhoaHocs;
using Project.Users;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Project
{
    public class ProjectApplicationAutoMapperProfile : Profile
    {
        public ProjectApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Role, RoleDto>();
            CreateMap<Role, RoleLookupDto>();
            CreateMap<CreateUpdateRoleDto, Role>();
            CreateMap<NguoiDungs.User, UserDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Category, CategoryLookupDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<AppUser, KhoaHocs.UserLookupDto>();
            CreateMap<Lesson, LessonDto>();
            CreateMap<Lesson, LessonLookupDto>();
            CreateMap<JoinCourse, JoinCourseDto>();
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<Rate, RateDto>();
            CreateMap<Contacts.Contact, ContactDto>();
            CreateMap<Course, CourseLookupDto>();
            CreateMap<Attachment, AttachmentLookupDto>();
            CreateMap<Course, UserLookupDto>();
        }
    }
}