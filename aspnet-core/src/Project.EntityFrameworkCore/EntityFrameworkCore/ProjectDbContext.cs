using Microsoft.EntityFrameworkCore;
using Project.DuLieu;
using Project.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using Project.BaiHocs;
using Project.DanhGias;
using Project.DanhMucs;
using Project.KhoaHocs;
using Project.ThamGiaKhoaHocs;
using System.Linq;
using Project.Comments;
using Project.Attachments;
using Project.Contacts;

namespace Project.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See ProjectMigrationsDbContext for migrations.
     */

    [ConnectionStringName("Default")]
    public class ProjectDbContext : AbpDbContext<ProjectDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Cources { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<JoinCourse> JoinCourses { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside ProjectDbContextModelCreatingExtensions.ConfigureProject
         */

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                //b.Property(x => x.UserName).IsRequired();
                //b.Property(x => x.Name).IsRequired();
                //b.Property(x => x.Email).IsRequired();
                //b.Property(x => x.PhoneNumber).IsRequired();
                b.Property(x => x.DateOfBirth);
                b.Property(x => x.Avatar);
                b.ConfigureByConvention();
                b.ConfigureAbpUser();
                /* Configure mappings for your additional properties
                 * Also see the ProjectEfCoreEntityExtensionMappings class
                 */
                b.ConfigureFullAuditedAggregateRoot();
                b.ConfigureExtraProperties();
                b.Ignore(b => b.ExtraProperties);


            });

            /* Configure your own tables/entities inside the ConfigureProject method */
            //foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}
            builder.Entity<IdentityUser>().Ignore(x => x.ExtraProperties);
            builder.Entity<IdentityUserLogin>().HasKey(x => new { x.UserId, x.LoginProvider });
            builder.Entity<IdentityUserRole>().HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserToken>().HasKey(x => new { x.UserId, x.LoginProvider });
            builder.Entity<IdentityUserOrganizationUnit>().HasKey(x => new { x.UserId, x.OrganizationUnitId });
            builder.ConfigureProject();
        }
    }
}