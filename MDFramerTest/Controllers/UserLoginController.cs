using MD.Services.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MD.Core.DomainModel;
using MD.Services.Message;

namespace MD.Web.Controllers
{
    public class UserLoginController : ApiController
    {
        private readonly IUserService _registerService;
        public UserLoginController(IUserService registerService)
        {
            this._registerService = registerService;
        }

        public string Login([FromBody]Account account)
        {
            if (account != null)
            {
                int ret = 0;
                try
                {
                    ret = this._registerService.UserLogin(account);
                }
                catch (ApplicationException ex)
                {
                    return ex.ToString();
                }
               
                if (ret == 0)
                {
                    return "登陆成功";
                }
            }
            return "没有提交登陆信息";
        }

        [HttpGet]
        public void SendMail()
        {
            List<UserInfo> list = _registerService.GetUserInfoList();
            if (list != null)
            {
                foreach (UserInfo item in list)
                {
                    if ((item.Birthday != null)
                        && (item.Birthday.ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd"))))
                    {
                        item.Account = _registerService.GetUserAccountById(item.Id);
                        if (item.Account != null)
                        {
                            string email = item.Account.Email;
                            string content = string.Format("{0}:<br>祝你生日快乐<br>", item.Name);
                            MailHelper.Intance.Send("生日快了", content, email);
                        }
                    }
                }

            }
        }
    }
}
