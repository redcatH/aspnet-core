using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application.Contracts;
using Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application.Contracts.Authorization;
using Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;

namespace Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application
{

    [Authorize(AuditLogPermission.AuditLogs.Default)]
    public class AuditLoggingAppService : ApplicationService, IAuditLoggingAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        
        public AuditLoggingAppService(IAuditLogRepository auditLogRepository)
        {
            this._auditLogRepository = auditLogRepository;
            ObjectMapperContext = typeof(AuditLoggingModule);
        }
        public async Task<PagedResultDto<AuditLogDto>> GetListAsync(PagedResultRequestDto input)
        {
            var total = await _auditLogRepository.GetCountAsync();
            var auditlog =await _auditLogRepository.PageBy(input).ToListAsync();
            var outputDto = ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(auditlog);
            PagedResultDto<AuditLogDto> pagedResultDto = new PagedResultDto<AuditLogDto>(total, outputDto);
            return pagedResultDto;
        }

        public async Task<Dictionary<DateTime,double>> GetAverageExecutionDurationPerDayAsync(DateTime startDate,DateTime endDate)
        {
            return await _auditLogRepository.GetAverageExecutionDurationPerDayAsync(startDate, endDate);
        }
    }
}
