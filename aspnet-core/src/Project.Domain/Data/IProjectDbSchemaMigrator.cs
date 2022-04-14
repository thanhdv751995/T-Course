using System.Threading.Tasks;

namespace Project.Data
{
    public interface IProjectDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
