using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MD.Services.Register;
using MD.Core.DomainModel;
using NUnit.Framework;
using Rhino.Mocks;
using System.Web.Mvc;
using MD.Core.Infrastructure.DependencyManagement;
using MD.Web.Frameworker;
using Autofac.Integration.Mvc;
using Autofac;
using System.Configuration;
using MD.Data;
using MD.Core.Data;
using MD.Services.ValueException;
using System.Collections.Generic; 

namespace MD.Web.Tests.Services
{
    [TestClass]
    public class UserServiceUnitTest
    {
        private IUserService _userService;
        private  IRepository<Account> _accountRepository;
        private  IRepository<UserInfo> _UserInforepository;
          
        private static string connstr = "Data Source=(LocalDb)\\v11.0;AttachDbFilename=D:\\Project\\test\\MDFramerTest.Tests\\aspnet-MDFramerTest-20151023025442.mdf;Initial Catalog=aspnet-MDFramerTest-20151023025442;Integrated Security=True";
        

       
        [TestMethod]
        public void TestGetUserInfo1()
        {
            var userRepository = MockRepository.GenerateStub<IRepository<UserInfo>>();
            userRepository.Stub(ur => ur.GetById(Arg<int>.Is.Anything)).Return(new UserInfo() { Name = "wangwu" });

            var accountRepository = MockRepository.GenerateStub<IRepository<Account>>();
            accountRepository.Stub(ur => ur.GetById(Arg<int>.Is.Anything)).Return(new Account() { Email = "wangwu@mingdao.com" });
            _userService = new UserService(accountRepository, userRepository);
            UserInfo user = _userService.GetUserInfo(2);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(user,null);
        }

        [TestMethod]
        public void TestGetUserInfo2()
        {
            MDObjectContext dbContext = new MDObjectContext(connstr);
            _UserInforepository = new EfRepository<UserInfo>(dbContext);
            _accountRepository = new EfRepository<Account>(dbContext);
            _userService = new UserService(_accountRepository, _UserInforepository);
            UserInfo user = null;
            try
            {
                user = _userService.GetUserInfo(2);
                
            }
            catch (Exception ex)
            {
                ex = ex;
            }
            finally
            {
                dbContext.Dispose();
                dbContext = null;
            }
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, user.Id);
        }

        public void TestGetUserInfo3()
        {
            MDObjectContext dbContext = new MDObjectContext(connstr);
            _UserInforepository = new EfRepository<UserInfo>(dbContext);
            _accountRepository = new EfRepository<Account>(dbContext);
            _userService = new UserService(_accountRepository, _UserInforepository);
            try
            {
                UserInfo user = _userService.GetUserInfo(-1);
            }
            catch (NumRangeExcetion ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "输入的账户ID超过有效范围[1-" + int.MaxValue + "]");
            }
            finally
            {
                dbContext.Dispose();
                dbContext = null;
            }

        }

        [TestMethod]
        public void TestUserRegister1()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
            userRepository.Expect(ur => ur.Insert(Arg<UserInfo>.Is.Anything));

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
            accountRepository.Expect(ur => ur.Insert(Arg<Account>.Is.Anything));

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserRegister(new Account() { Email =string.Empty, Password = "123456" });
            }
            catch (ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "注册的邮箱不能为空");
            }

        }

        [TestMethod]
        public void TestUserRegister2()
        {   var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
            userRepository.Expect(ur => ur.Insert(Arg<UserInfo>.Is.Anything));

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
            accountRepository.Expect(ur => ur.Insert(Arg<Account>.Is.Anything));

            var userService = new UserService(accountRepository,userRepository);
            try 
            {
                userService.UserRegister(new Account() { Email = "zhangsan", Password = "123456" });
            }
             catch(ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "注册的邮箱格式错误，必须为有效的邮箱");
            }
            
            
        }

        [TestMethod]
        public void TestUserRegister3()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
            userRepository.Expect(ur => ur.Insert(Arg<UserInfo>.Is.Anything));

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
            accountRepository.Expect(ur => ur.Insert(Arg<Account>.Is.Anything));

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserRegister(new Account() { Email = "zhangsan@gg.com", Password = "" });
            }
            catch (ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "注册的密码不能为空");
            }


        }
        [TestMethod]
        public void TestUserRegister4()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
            userRepository.Expect(ur => ur.Insert(Arg<UserInfo>.Is.Anything));

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
            accountRepository.Expect(ur => ur.Insert(Arg<Account>.Is.Anything));

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserRegister(new Account() { Email = "zhangsan@gg.com", Password = "123456" });
            }
            catch (ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "注册的密码和确认密码不一致");
            }


        }

        [TestMethod]
        public void TestUserRegister5()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
            userRepository.Expect(ur => ur.Insert(Arg<UserInfo>.Is.Anything));

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
            accountRepository.Expect(ur => ur.Insert(Arg<Account>.Is.Anything));

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserRegister(new Account() { Email = "zhangsan@gg.com", Password = "123456", ConfirmPassword="654321" });
            }
            catch (ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "注册的密码和确认密码不一致");
            }


        }

        [TestMethod]
        public void TestUserRegister6()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
            userRepository.Expect(ur => ur.Insert(Arg<UserInfo>.Is.Anything));

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
            accountRepository.Expect(ur => ur.Insert(Arg<Account>.Is.Anything));

            var userService = new UserService(accountRepository, userRepository);
            
            userService.UserRegister(new Account() { Email = "zhangsan@gg.com", Password = "123456", ConfirmPassword = "123456" });
           

            accountRepository.VerifyAllExpectations();

        }

        [TestMethod]
        public void TestUserRegister7()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserRegister(null);
            }
            catch (NullParamException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "注册的账户信息不能为NULL");
            }


        }

        [TestMethod]
        public void TestUserRegister8()
        {
            MDObjectContext dbContext = new MDObjectContext(connstr);
            _UserInforepository = new EfRepository<UserInfo>(dbContext);
            _accountRepository = new EfRepository<Account>(dbContext);
            _userService = new UserService(_accountRepository, _UserInforepository);
            int ret = 0;
            try
            {
                Account account = new Account() { Email = "ee1@mingdao.com", Password = "123456", ConfirmPassword = "123456" };
                ret = _userService.UserRegister(account);

            }
            catch (ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "该邮箱已经注册过，不能重复注册");
            }
            finally
            {
                dbContext.Dispose();
                dbContext = null;
            }
        }

        [TestMethod]
        public void TestUserLogin1()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserLogin(null);
            }
            catch (NullParamException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "登陆的账户信息不能为NULL");
            }


        }

        [TestMethod]
        public void TestUserLogin2()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserLogin(new Account() { Email = "", Password = "123456", ConfirmPassword = "123456" });
            }
            catch (ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "登陆的邮箱不能为空");
            }


        }

        [TestMethod]
        public void TestUserLogin3()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserLogin(new Account() { Email = "aa@te.com", Password = "" });
            }
            catch (ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "登陆的密码不能为空");
            }

        }
         [TestMethod]
        public void TestUserLogin4()
        {
            var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();

            var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();

            var userService = new UserService(accountRepository, userRepository);
            try
            {
                userService.UserLogin(new Account() { Email = "aa@te.com", Password = "123456"});
            }
            catch (ValueFormatException ex)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "输入的登陆账户不存在");
            }


        }

         [TestMethod]
         public void TestUserLogin5()
         {
             MDObjectContext dbContext = new MDObjectContext(connstr);
             _UserInforepository = new EfRepository<UserInfo>(dbContext);
             _accountRepository = new EfRepository<Account>(dbContext);
             _userService = new UserService(_accountRepository, _UserInforepository);
             int ret = 0;
             try
             {
                 Account account = new Account() { Email = "ee1@ww.com", Password = "123456" };
                 ret = _userService.UserLogin(account);

             }
             catch (ValueFormatException ex)
             {
                 Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "输入的登陆账户不存在");
             }
             finally
             {
                 dbContext.Dispose();
                 dbContext = null;
             }
         }

         [TestMethod]
         public void TestUserLogin6()
         {
             MDObjectContext dbContext = new MDObjectContext(connstr);
             _UserInforepository = new EfRepository<UserInfo>(dbContext);
             _accountRepository = new EfRepository<Account>(dbContext);
             _userService = new UserService(_accountRepository, _UserInforepository);
             int ret = 0;
             try
             {
                 Account account = new Account() { Email = "ee1@mingdao.com", Password = "654321" };
                 ret = _userService.UserLogin(account);

             }
             catch (ValueFormatException ex)
             {
                 Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "输入的登陆密码错误");
             }
             finally
             {
                 dbContext.Dispose();
                 dbContext = null;
             }
         }

         [TestMethod]
         public void TestUserLogin7()
         {
             MDObjectContext dbContext = new MDObjectContext(connstr);
             _UserInforepository = new EfRepository<UserInfo>(dbContext);
             _accountRepository = new EfRepository<Account>(dbContext);
             _userService = new UserService(_accountRepository, _UserInforepository);
             int ret = 0;
             try
             {
                 Account account = new Account() { Email = "ee1@mingdao.com", Password = "123456" };
                 ret = _userService.UserLogin(account);

             }
             catch (ValueFormatException ex)
             {
                 Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(false);
             }
             finally
             {
                 dbContext.Dispose();
                 dbContext = null;
             }

             Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(ret ==0);
         }


         [TestMethod]
         public void TestUserDel1()
         {
             var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
             userRepository.Expect(ur => ur.Delete(Arg<UserInfo>.Is.Anything));

             var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
             accountRepository.Expect(ur => ur.GetById(Arg<int>.Is.Anything));
             accountRepository.Expect(ur => ur.Delete(Arg<Account>.Is.Anything));

             var userService = new UserService(accountRepository, userRepository);
             userService.UserDel(2);

             //userRepository.VerifyAllExpectations();
             accountRepository.VerifyAllExpectations();
         }

         [TestMethod]
         public void TestUserDel2()
         {
             var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
             userRepository.Expect(ur => ur.Delete(Arg<UserInfo>.Is.Anything));

             var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
             accountRepository.Expect(ur => ur.GetById(Arg<int>.Is.Anything));
             accountRepository.Expect(ur => ur.Delete(Arg<Account>.Is.Anything));

             var userService = new UserService(accountRepository, userRepository);
             try
             {
                 userService.UserDel(0);
             }
             catch(NumRangeExcetion ex)
             {
                 Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "输入的账户ID超过有效范围[1-"+int.MaxValue+"]");
             }

         }
         public class testclass : UserService
         {
            private readonly IRepository<MD.Core.DomainModel.Account> _registerRepository;

            private readonly IRepository<MD.Core.DomainModel.UserInfo> _userRepository;
            
            public testclass(IRepository<MD.Core.DomainModel.Account> registerRepository,
                        IRepository<MD.Core.DomainModel.UserInfo> userRepository)
                : base(registerRepository, userRepository)
            {
                this._registerRepository = registerRepository;
                this._userRepository = userRepository;
            }
             public override UserInfo GetUserInfo(int accountId)
             {
                 return new UserInfo();
             }
         }
         [TestMethod]
         public void TestUserDel3()
         {
            
             var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
             userRepository.Expect(ur => ur.Delete(Arg<UserInfo>.Is.Anything));

             var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
             accountRepository.Expect(ur => ur.GetById(Arg<int>.Is.Anything));
             accountRepository.Expect(ur => ur.Delete(Arg<Account>.Is.Anything));


             var userService = MockRepository.GeneratePartialMock<testclass>(accountRepository, userRepository);
             
             userService.UserDel(2);

             userRepository.VerifyAllExpectations();
             accountRepository.VerifyAllExpectations();

         }

         [TestMethod]
         public void TestUserDel4()
         {

             var userRepository = MockRepository.GenerateMock<IRepository<UserInfo>>();
             userRepository.Expect(ur => ur.Delete(Arg<UserInfo>.Is.Anything));

             var accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
             accountRepository.Expect(ur => ur.GetById(Arg<int>.Is.Anything)).Return(null);
             accountRepository.Expect(ur => ur.Delete(Arg<Account>.Is.Anything));


             var userService = MockRepository.GeneratePartialMock<testclass>(accountRepository, userRepository);

             try
             {
                 userService.UserDel(4);
             }
             catch (ValueFormatException ex)
             {
                 Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ex.Message, "要删除的账户不存在");
             }

         }

         [TestMethod]
         public void TestGetUserInfoList1()
         {
             var userRepository = MockRepository.GenerateStub<IRepository<UserInfo>>();

             var accountRepository = MockRepository.GenerateStub<IRepository<Account>>();

             _userService = new UserService(accountRepository, userRepository);
             List<UserInfo> userList = _userService.GetUserInfoList();
             Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(userList, null);
         }

         [TestMethod]
         public void TestGetUserInfoList2()
         {
             MDObjectContext dbContext = new MDObjectContext(connstr);
             _UserInforepository = new EfRepository<UserInfo>(dbContext);
             _accountRepository = new EfRepository<Account>(dbContext);
             _userService = new UserService(_accountRepository, _UserInforepository);

             List<UserInfo> userList = null;
             try
             {
                 userList = _userService.GetUserInfoList();
                 
             }
             catch (Exception ex)
             {
                 ex = ex;
             }             
             finally
             {
                 dbContext.Dispose();
                 dbContext = null;
             }
             Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(userList.Count > 0);

         }

         
    }
}
