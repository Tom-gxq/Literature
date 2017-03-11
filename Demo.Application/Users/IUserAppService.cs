using Demo.Application.Users.Dto;
using Demo.Core.Identity.Users;
using Lib.Application.Services;
using LibMain.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Users
{
    public interface IUserAppService : IApplicationService
    {
        ListResultOutput<UserDto> GetUsers(string idstr);
    }
}
