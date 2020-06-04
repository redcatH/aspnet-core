using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Redcat.Abp.AppManagement.Domain;

namespace Redcat.Abp.AppManagement.Application
{
    public class AppManagementAutoMapperProfile: Profile
    {
        public AppManagementAutoMapperProfile()
        {
            CreateMap<App, AppDto>();
            CreateMap<CreateOrUpdateAppDto, App>();
        }
    }
}
