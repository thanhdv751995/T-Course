using Project.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Project.Permissions
{
    public class ProjectPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var ProjectGroup = context.AddGroup(ProjectPermissions.GroupName, L("Permission:Manager"));

            var rolesPermission = ProjectGroup.AddPermission(
                ProjectPermissions.Roles.Default, L("Permission:Roles"));
            rolesPermission.AddChild(ProjectPermissions.Roles.Create, L("Permission:Roles.Create"));
            rolesPermission.AddChild(ProjectPermissions.Roles.Edit, L("Permission:Roles.Edit"));
            rolesPermission.AddChild(ProjectPermissions.Roles.Delete, L("Permission:Roles.Delete"));
            //Define your own permissions here. Example:
            //myGroup.AddPermission(ProjectPermissions.MyPermission1, L("Permission:MyPermission1"));


            var usersPermission = ProjectGroup.AddPermission(
                ProjectPermissions.Users.Default, L("Permission:Users"));

            usersPermission.AddChild(
                ProjectPermissions.Users.Create, L("Permission:Users.Create"));

            usersPermission.AddChild(
                ProjectPermissions.Users.Edit, L("Permission:Users.Edit"));

            usersPermission.AddChild(
                ProjectPermissions.Users.Delete, L("Permission:Users.Delete"));

            var categoriesPermission = ProjectGroup.AddPermission(
                ProjectPermissions.Categories.Default, L("Permission:Categories"));

            categoriesPermission.AddChild(
                ProjectPermissions.Categories.Create, L("Permission:Categories.Create"));

            categoriesPermission.AddChild(
                ProjectPermissions.Categories.Edit, L("Permission:Categories.Edit"));

            categoriesPermission.AddChild(
                ProjectPermissions.Categories.Delete, L("Permission:Categories.Delete"));

            var coursesPermission = ProjectGroup.AddPermission(
                ProjectPermissions.Courses.Default, L("Permission:Courses"));

            coursesPermission.AddChild(
                ProjectPermissions.Courses.Create, L("Permission:Courses.Create"));

            coursesPermission.AddChild(
                ProjectPermissions.Courses.Edit, L("Permission:Courses.Edit"));

            coursesPermission.AddChild(
                ProjectPermissions.Courses.Delete, L("Permission:Courses.Delete"));
            //var CommentsPermission = ProjectGroup.AddPermission(
            //  ProjectPermissions.Comments.Default, L("Permission:Comments"));

            //CommentsPermission.AddChild(
            //    ProjectPermissions.Comments.Create, L("Permission:Comments.Create"));

            //CommentsPermission.AddChild(
            //    ProjectPermissions.Comments.Edit, L("Permission:Comments.Edit"));

            //CommentsPermission.AddChild(
            //    ProjectPermissions.Comments.Delete, L("Permission:Comments.Delete"));

            var attachmentsPermission = ProjectGroup.AddPermission(ProjectPermissions.Attachments.Default, 
                L("Permission:Attachments"));

            attachmentsPermission.AddChild(
                ProjectPermissions.Attachments.Create, L("Permission:Attachments.Create"));

            attachmentsPermission.AddChild(
                ProjectPermissions.Attachments.Edit, L("Permission:Attachments.Edit"));

            attachmentsPermission.AddChild(
                ProjectPermissions.Attachments.Delete, L("Permission:Attachments.Delete"));

            var lessonsPermission = ProjectGroup.AddPermission(ProjectPermissions.Lessons.Default,
                L("Permission:Lessons"));

            lessonsPermission.AddChild(
                ProjectPermissions.Lessons.Create, L("Permission:Lessons.Create"));

            lessonsPermission.AddChild(
                ProjectPermissions.Lessons.Edit, L("Permission:Lessons.Edit"));

            lessonsPermission.AddChild(
                ProjectPermissions.Lessons.Delete, L("Permission:Lessons.Delete"));
        }


        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProjectResource>(name);
        }
    }
}
