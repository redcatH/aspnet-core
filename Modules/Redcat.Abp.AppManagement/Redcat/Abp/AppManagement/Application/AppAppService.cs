﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redcat.Abp.AppManagement.Application.Contracts.Authorization;
using Redcat.Abp.AppManagement.Apps;
using Redcat.Abp.AppManagement.Domain;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Redcat.Abp.AppManagement.Application
{
    public class AppAppService: CrudAppService<App,AppDto, Guid,PagedAndSortedResultRequestDto, CreateOrUpdateAppDto, CreateOrUpdateAppDto>
    {
        private readonly IAppDefinitionManager _appDefinitionManager;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;
        public AppAppService(IAppDefinitionManager appDefinitionManager,IRepository<App,Guid> appRepository, ICurrentTenant currentTenant, ICurrentUser currentUser) : base(appRepository)
        {
            this._appDefinitionManager = appDefinitionManager;
            _currentTenant = currentTenant;
            _currentUser = currentUser;
            ObjectMapperContext = typeof(AppManagementModule);
            base.CreatePolicyName = AppManagentPermission.AppManagent.Create;
            base.UpdatePolicyName = AppManagentPermission.AppManagent.Update;
            base.GetListPolicyName = AppManagentPermission.AppManagent.Default;
            base.DeletePolicyName = AppManagentPermission.AppManagent.Delete;
        }


        public async Task<List<AppDefinition>> GetDefinitions()
        {
            var list= await _appDefinitionManager.GetList();
            return list?.ToList();
        }

        
        protected override IQueryable<App> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return this.Repository
                .WhereIf(_currentTenant.Id.HasValue,
                    app => app.ProviderName == "T" && app.ProviderKey == this._currentTenant.Name.ToString())
                .WhereIf(!_currentTenant.Id.HasValue && !string.IsNullOrEmpty(_currentUser.UserName),
                    app => app.ProviderName == "U" && app.ProviderKey == this._currentUser.UserName)
                .WhereIf(!_currentTenant.Id.HasValue, 
                    app => app.ProviderName == "T" && app.ProviderKey == "");

        }
    }


}