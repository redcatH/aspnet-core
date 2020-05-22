using AutoMapper;
using Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AuditLogging;

namespace Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application
{
    public class AuditLoggingAutoMapperProfile: Profile
    {
        public AuditLoggingAutoMapperProfile()
        {
            CreateMap<AuditLog, AuditLogDto>()
                .ForMember(d => d.EntityChanges, option => option.MapFrom(m => m.EntityChanges))
                .ForMember(d => d.Actions, option => option.MapFrom(m => m.Actions));

            CreateMap<EntityChange, EntityChangeDto>().
                ForMember(d => d.PropertyChanges, option => option.MapFrom(m => m.PropertyChanges));

            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();
            CreateMap<AuditLogAction, AuditLogActionDto>();
            
        }
    }
}
