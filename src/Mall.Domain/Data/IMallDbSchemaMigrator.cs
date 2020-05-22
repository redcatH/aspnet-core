using System.Threading.Tasks;

namespace Mall.Data
{
    public interface IMallDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
