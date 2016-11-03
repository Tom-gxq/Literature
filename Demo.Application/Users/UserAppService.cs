using Demo.Application.Users.Dto;
using Lib.Application.Services;
using LibMain.Application.Services.Dto;
using LibMain.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {

        public UserAppService()
        {
            
        }

        public ListResultOutput<UserDto> GetUsers()
        {
            return new ListResultOutput<UserDto>
            {
                Items = null
            };
        }
    }
}
