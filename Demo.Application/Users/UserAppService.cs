using Demo.Application.Users.Dto;
using Demo.Core.Identity.Users;
using Lib.Application.Services;
using LibMain.Application.Services.Dto;
using LibMain.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.EntityFramework.EntityFramework.Repositories;
using Lib.EntityFramework.EntityFramework.Uow;
using Lib.EntityFramework.EntityFramework;

namespace Demo.Application.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IRepository<User2> _userRepository;
        public UserAppService(IRepository<User2> userRepository)
        {
            _userRepository = userRepository;
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
