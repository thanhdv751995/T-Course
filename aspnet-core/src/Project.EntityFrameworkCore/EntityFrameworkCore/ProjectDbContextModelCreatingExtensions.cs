
using Microsoft.EntityFrameworkCore;
using Project.Attachments;
using Project.BaiHocs;
using Project.Comments;
using Project.Contacts;
using Project.DanhGias;
using Project.DanhMucs;
using Project.DuLieu;
using Project.KhoaHocs;
using Project.NguoiDung;
using Project.ThamGiaKhoaHocs;
using Project.Users;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Project.EntityFrameworkCore
{
    public static class ProjectDbContextModelCreatingExtensions
    {
        public static void ConfigureProject(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<Role>(b =>
            //{
            //    b.ToTable(ProjectConsts.DbTablePrefix + "MyRoles", ProjectConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //    b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            //});
            //builder.Entity<NguoiDungs.User>(b =>
            //{
            //    b.ToTable(ProjectConsts.DbTablePrefix + "MyUsers",
            //    ProjectConsts.DbSchema);
            //    b.ConfigureByConvention();

            //    b.Property(x => x.Name)
            //        .IsRequired()
            //        .HasMaxLength(UserConst.MaxNameLength);

            //    b.HasIndex(x => x.Name);
            //    b.HasOne<Role>().WithMany().HasForeignKey(x => x.IDRole).IsRequired();
            //});
            builder.Entity<Lesson>(b =>
            {
                b.ToTable(ProjectConsts.DbTablePrefix + "Lessons", ProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
                b.Property(x => x.Name).IsRequired().HasMaxLength(3000);
                b.Property(x => x.Description).IsRequired().HasMaxLength(3000);
                b.HasOne<Course>().WithMany().HasForeignKey(x => x.IDCourse).IsRequired();
            });
            builder.Entity<Comment>(b =>
            {
                b.ToTable(ProjectConsts.DbTablePrefix + "Comments", ProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
                b.Property(x => x.content).IsRequired().HasMaxLength(3000);
                b.Property(x => x.IDParent);
                b.HasOne<Comment>().WithMany().HasForeignKey(x => x.IDParent).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Lesson>().WithMany().HasForeignKey(x => x.IDLesson).IsRequired();
                b.HasOne<Volo.Abp.Identity.IdentityUser>().WithMany().HasForeignKey(x => x.IDUser).OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Rate>(b =>
            {
                b.ToTable(ProjectConsts.DbTablePrefix + "Rates", ProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
                b.Property(x => x.RatePoint).HasMaxLength(6);
                b.Property(x => x.Content).HasMaxLength(3000);
                b.Property(x => x.IDUser);
                b.Property(x => x.IDCourse);
                b.HasOne<Volo.Abp.Identity.IdentityUser>().WithMany().HasForeignKey(x => x.IDUser).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Course>().WithMany().HasForeignKey(x => x.IDCourse).IsRequired();
            });
            builder.Entity<Category>(b =>
            {
                b.ToTable(ProjectConsts.DbTablePrefix + "Categories", ProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
                b.Property(x => x.Name).IsRequired().HasMaxLength(300);
                b.Property(x => x.Description).HasMaxLength(3000);
                b.Property(x => x.IDParent);
                b.HasOne<Category>().WithMany().HasForeignKey(x => x.IDParent).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Course>(b =>
            {
                b.ToTable(ProjectConsts.DbTablePrefix + "Courses", ProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
                b.Property(x => x.Name).IsRequired().HasMaxLength(300);
                b.Property(x => x.Description).IsRequired().HasMaxLength(3000);
                b.HasOne<Category>().WithMany().HasForeignKey(x => x.IDCategory).IsRequired();
                b.HasOne<Volo.Abp.Identity.IdentityUser>().WithMany().HasForeignKey(x => x.IDUser).IsRequired();
            });
            builder.Entity<Attachment>(b =>
            {
                b.ToTable(ProjectConsts.DbTablePrefix + "Attachments", ProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
                b.Property(x => x.URL).IsRequired();
                b.Property(x => x.IDTable).IsRequired();
            });
            builder.Entity<JoinCourse>(b =>
            {
                b.ToTable(ProjectConsts.DbTablePrefix + "JoinCourses", ProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...

                b.HasOne<Volo.Abp.Identity.IdentityUser>().WithMany().HasForeignKey(x => x.IDUser).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Course>().WithMany().HasForeignKey(x => x.IDCourse).IsRequired();
            });
            builder.Entity<Contact>(b =>
            {
                b.ToTable(ProjectConsts.DbTablePrefix + "Contacts", ProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });
        }
    }
}