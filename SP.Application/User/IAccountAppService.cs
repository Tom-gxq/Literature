using Lib.Application.Services;
using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User
{
    public interface IAccountAppService :IApplicationService
    {
        List<AccountInfoDto> SearchAccount(string keywords);
        AccountInfoDto GetAccountInfo(string accountId);
        bool UpdateAccountUserType(string accountId, int userType);
    }
}
