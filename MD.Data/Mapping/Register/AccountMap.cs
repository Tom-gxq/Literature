using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Data.Mapping.Register
{
    public partial class AccountMap : MDEntityTypeConfiguration<MD.Core.DomainModel.Account>
    {
        public AccountMap()
        {
            this.ToTable("TAccount");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Email).IsRequired();
            this.Property(bp => bp.Password).IsRequired();
        }
    }
}
