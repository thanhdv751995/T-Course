using Project.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Project
{
    [DependsOn(
        typeof(ProjectEntityFrameworkCoreTestModule)
        )]
    public class ProjectDomainTestModule : AbpModule
    {

    }
}