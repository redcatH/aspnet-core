using AutoMapper;
using Mall.Dto.AppUser;
using Mall.Users;

namespace Mall
{
    public class MallApplicationAutoMapperProfile : Profile
    {
        public MallApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<AppUser, AppUserDto>();
        }
    }
}
