using Demo.Core.Identity.Authorization;
using Demo.Core.Identity.Users;
using Lib.EntityFramework.EntityFramework.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.EntityFramework
{
    public class DemoDbContext : Lib.EntityFramework.EntityFramework.LibDbContext
    {
        public  IDbSet<UserLogin> UserLogins { get; set; }
        /// <summary>
        /// Users.
        /// </summary>
        public  IDbSet<User2> Users { get; set; }

        public DemoDbContext()
            : base("Default")
        {

        }
        public DemoDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        

    }
}
