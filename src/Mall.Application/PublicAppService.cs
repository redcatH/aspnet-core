using Mall.Dto.AppUser;
using Mall.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Mall
{
    [Authorize]
    public class PublicAppService: ApplicationService
    {
        private readonly IRepository<AppUser, Guid> _identityUsers;
        public PublicAppService(IRepository<AppUser, Guid> identityUsers)
        {
            _identityUsers = identityUsers;
        }


        
        /// <summary>
        /// 获取当前用户的信息
        /// </summary>
        /// <returns></returns>
        public async Task<AppUserDto> GetCurrentUser()
        {
            var user= await _identityUsers.FirstOrDefaultAsync(p => p.Id == CurrentUser.Id);
            var obj=ObjectMapper.Map<AppUser, AppUserDto>(user);
            return obj;
        }
    }
}
