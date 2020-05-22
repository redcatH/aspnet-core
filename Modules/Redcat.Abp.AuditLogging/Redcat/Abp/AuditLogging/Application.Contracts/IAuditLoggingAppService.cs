using Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application.Contracts
{
    public interface IAuditLoggingAppService:IApplicationService
    {
        Task<PagedResultDto<AuditLogDto>> GetListAsync(PagedResultRequestDto input);
    }
}
