using System.Threading.Tasks;
using Redcat.Abp.AuditLogging.Application.Contracts.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Redcat.Abp.AuditLogging.Application.Contracts
{
    public interface IAuditLoggingAppService:IApplicationService
    {
        Task<PagedResultDto<AuditLogDto>> GetListAsync(PagedResultRequestDto input);
    }
}
