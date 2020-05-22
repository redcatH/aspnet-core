using System;
using System.Collections.Generic;
using System.Text;

namespace Mall.Dto.AppUser
{
    public class AppUserDto
    {
        public  Guid? TenantId { get; private set; }

        public  string UserName { get; private set; }

        public  string Name { get; private set; }

        public  string Surname { get; private set; }

        public  string Email { get; private set; }

        public  bool EmailConfirmed { get; private set; }

        public  string PhoneNumber { get; private set; }

        public  bool PhoneNumberConfirmed { get; private set; }

        public  string Nickname { get; set; }
        public  string HeadImgUrl { get; set; }
    }
}
