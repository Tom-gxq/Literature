using MD.Core.Data;
using MD.Core.DomainModel;
using MD.Services.ValueException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MD.Services.Register
{
    public partial class UserService : IUserService
    {
        private readonly IRepository<MD.Core.DomainModel.Account> _registerRepository;

        private readonly IRepository<MD.Core.DomainModel.UserInfo> _userRepository;
        public UserService(IRepository<MD.Core.DomainModel.Account> registerRepository,
                    IRepository<MD.Core.DomainModel.UserInfo> userRepository)
        {
            this._registerRepository = registerRepository;
            this._userRepository = userRepository;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="account">账户信息</param>
        /// <returns>0: 成功 其他:失败</returns>
        public int UserRegister(Account account)
        {
            if(account == null)
            {
                throw new NullParamException("注册的账户信息不能为NULL");
            }
            
            if(string.IsNullOrEmpty(account.Email))
            {
                throw new ValueFormatException("注册的邮箱不能为空");
            }
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

            if (!r.IsMatch(account.Email))
            {
               ValueFormatException exception =  new ValueFormatException("注册的邮箱格式错误，必须为有效的邮箱");
               exception.FormatContent = account.Email;
               throw exception;
            }

            if (string.IsNullOrEmpty(account.Password))
            {
                throw new ValueFormatException("注册的密码不能为空");
            }

            if (!account.Password.Equals(account.ConfirmPassword))
            {
                ValueFormatException exception = new ValueFormatException("注册的密码和确认密码不一致");
                exception.FormatContent = string.Format("Pwd:[{0}]    ConfirmPwd:[{1}]", account.Password, account.ConfirmPassword);
                throw exception;
            }

            IQueryable<Account> query = this._registerRepository.TableNoTracking;
            if ((query != null) && (query.Count() > 0))
            {
                List<Account> list = query.Where<Account>(o => o.Email == account.Email).ToList<Account>();
                if ((list != null) && (list.Count() > 0))
                {
                    throw new ValueFormatException("该邮箱已经注册过，不能重复注册");
                }
            }
            try
            {
                this._registerRepository.Insert(account);
            }
            catch(Exception ex)
            {
                string err = ex.ToString();
            }

            return 0;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account">账户信息</param>
        /// <returns>0: 成功 其他:失败</returns>
        public int UserLogin(Account account)
        {
            if (account == null)
            {
                throw new NullParamException("登陆的账户信息不能为NULL");
            }            
            if (string.IsNullOrEmpty(account.Email))
            {
                throw new ValueFormatException("登陆的邮箱不能为空");
            }

            if (string.IsNullOrEmpty(account.Password))
            {
                throw new ValueFormatException("登陆的密码不能为空");
            }

            IQueryable<Account> query = this._registerRepository.TableNoTracking;
            if ((query != null) && (query.Count() > 0))
            {
                List<Account> list = query.Where<Account>(o => o.Email == account.Email).ToList<Account>();
                if ((list != null) && (list.Count>0))
                {
                    int ret = -1;
                    foreach (Account item in list)
                    {
                        if (item.Password == account.Password)
                        {
                            ret = 0;
                            break;
                        }
                    }
                    if(ret != 0)
                    {
                        throw new ValueFormatException("输入的登陆密码错误");
                    }
                }
                else
                {
                    throw new ValueFormatException("输入的登陆账户不存在");
                }
            }
            else
            {
                throw new ValueFormatException("输入的登陆账户不存在");
            }

            return 0;
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="accountId">注册的账户ID</param>
        /// <returns>0: 成功 其他:失败</returns>
        public int UserDel(int accountId)
        {
            if ((accountId < 1) || (accountId > int.MaxValue))
            {
                string err = string.Format("输入的账户ID超过有效范围[{0}-{1}]",1,int.MaxValue);
                NumRangeExcetion exception = new NumRangeExcetion(err);
                exception.NumRange = accountId.ToString();
                throw exception;
            }
            Account account = this._registerRepository.GetById(accountId);
            if (account != null)
            {
                this._registerRepository.Delete(account);
            }
            else
            {
                throw new ValueFormatException("要删除的账户不存在");
            }

            UserInfo user = GetUserInfo(accountId);
            if (user != null)
            {
                this._userRepository.Delete(user);
            }
            
            return 0;

        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public List<UserInfo> GetUserInfoList()
        {
            List<UserInfo> list = null;

            IQueryable<UserInfo> query = this._userRepository.TableNoTracking;
            if ((query != null) && (query.Count() > 0))
            {
                list = query.ToList<UserInfo>();
            }

            return list;
        }
        
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accountId">注册的账户ID</param>
        /// <returns>用户基本信息</returns>
        public virtual UserInfo GetUserInfo(int accountId)
        {
            if ((accountId < 1) || (accountId > int.MaxValue))
            {
                string err = string.Format("输入的账户ID超过有效范围[{0}-{1}]", 1, int.MaxValue);
                NumRangeExcetion exception = new NumRangeExcetion(err);
                exception.NumRange = accountId.ToString();
                throw exception;
            }
           
            UserInfo user = null;
            IQueryable<UserInfo> query = this._userRepository.TableNoTracking;
            if ((query != null) && (query.Count() > 0))
            {
                query = query.Where<UserInfo>(o => o.Account.Id == accountId);
                if(query.Count() > 0)
                {
                    user = query.Single();
                }                
            }

            return user;
        }
        public int AddUserInfo(UserInfo user)
        {
            int ret = 0;//scuccess return 0
            try
            {
                this._userRepository.Insert(user);
            }
            catch(Exception ex)
            {
                ret = -1;
            }
            return ret;
        }

        public Account GetUserAccount(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ValueFormatException("注册的邮箱不能为空");                
            }

            Account account = null;
            try
            {
                IQueryable<Account> query = this._registerRepository.Table;
                if ((query != null) && (query.Count() > 0))
                {
                    account = query.Where<Account>(o => o.Email == email).Single();
                }
            }
            catch (Exception ex)
            {

            }

            return account;
        }

        public List<Account> GetAccountList()
        {
            List<Account> list = null;

            IQueryable<Account> query = this._registerRepository.Table;
            if ((query != null) && (query.Count() > 0))
            {
                list = query.ToList<Account>();
            }

            return list;
        }

        public Account GetUserAccountById(int Id)
        {
            if ((Id < 1) || (Id > int.MaxValue))
            {
                string err = string.Format("输入的账户ID超过有效范围[{0}-{1}]", 1, int.MaxValue);
                NumRangeExcetion exception = new NumRangeExcetion(err);
                exception.NumRange = Id.ToString();
                throw exception;
            }

            return this._registerRepository.GetById(Id);
        }

        
    }
}
