using Lib.Application.Services;
using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User
{
    public interface IAdminAppService: IApplicationService
    {
        AdminDto GetCurrentSession();
        AdminDto GetUsers(string userName);
        bool CheckAdminLogin(string userName, string passWord);
        bool AddAdmin(string userName, string passWord);
        bool DelAdmin(int id);
        List<AdminDto> GetAdminList(int pageIndex, int pageSize);
        List<AdminDto> SearchAdminByUserName(string userName);
        int GetAdminListCount();
        int SearchAdminByUserNameCount(string userName);
        bool DelCurrentSession();
        AdminDto GetAdminDetail(int id);
        bool EditAdmin(AdminDto admin);
    }
}
