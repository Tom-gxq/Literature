using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Identity.Users
{
    public interface IUser<out TKey>
    {
        //
        // 摘要:
        //     Unique key for the user
        TKey Id { get; }
        //
        // 摘要:
        //     Unique username
        string UserName { get; set; }
    }
}
