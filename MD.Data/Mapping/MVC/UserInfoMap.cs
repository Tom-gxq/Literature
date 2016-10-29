using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Data.Mapping.MVC
{
    public partial class UserInfoMap : MDEntityTypeConfiguration<MD.Core.DomainModel.UserInfo>
    {
        public UserInfoMap()
        {
            this.ToTable("TUserInfo");
            this.HasKey(bp => bp.Id);
            this.HasRequired(t => t.Account).WithRequiredDependent(t => t.User);
        }
    }
}
