namespace Project.Permissions
{
    public static class ProjectPermissions
    {
        public const string GroupName = "Project";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        public static class Roles
        {
            public const string Default = GroupName + ".Roles";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
        public static class Users
        {
            public const string Default = GroupName + ".Users";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
        public static class Categories
        {
            public const string Default = GroupName + ".Categories";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
        public static class Courses
        {
            public const string Default = GroupName + ".Courses";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
        //public static class Comments
        //{
        //    public const string Default = GroupName + ".Comments";
        //    public const string Create = Default + ".Create";
        //    public const string Edit = Default + ".Edit";
        //    public const string Delete = Default + ".Delete";
        //}
        public static class Lessons
        {
            public const string Default = GroupName + ".Lessons";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
        public static class Attachments
        {
            public const string Default = GroupName + ".Attachments";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}