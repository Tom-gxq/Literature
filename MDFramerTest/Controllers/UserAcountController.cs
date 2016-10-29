using MD.Core.DomainModel;
using MD.Services.Demo;
using MD.Services.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MD.Web.Controllers
{
    public class UserAcountController : ApiController
    {
        private readonly IUserService _registerService;
        public UserAcountController(IUserService registerService)
        {
            this._registerService = registerService;
        }
        // GET api/register
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/register/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/register
        public string Register([FromBody]Account account)
        {
            if (account != null)
            {
                int ret = 0;
                try
                {
                    ret = this._registerService.UserRegister(account);
                }
                catch(ApplicationException ex)
                {
                    return ex.ToString();
                }
                
                if(ret == 0)
                {
                    return "注册成功";
                }
            }
            return "提交的信息为空";
        }

        // DELETE api/register/5
        public void Delete(int id)
        {
        }
    }
}
