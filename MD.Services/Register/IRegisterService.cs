using MD.Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Services.Register
{
    public interface IUserService
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="account">账户信息</param>
        /// <returns>0: 成功 其他:失败</returns>
        int UserRegister(Account account);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account">账户信息</param>
        /// <returns>0: 成功 其他:失败</returns>
        int UserLogin(Account account);

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="accountId">注册的账户ID</param>
        /// <returns>0: 成功 其他:失败</returns>
        int UserDel(int accountId);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        List<UserInfo> GetUserInfoList();

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accountId">注册的账户ID</param>
        /// <returns>用户基本信息</returns>
        UserInfo GetUserInfo(int accountId);

        int AddUserInfo(UserInfo user);

        Account GetUserAccount(string email);
        Account GetUserAccountById(int Id);
    }
}
