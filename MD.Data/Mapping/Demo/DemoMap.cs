using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD.Core.DomainModel;

namespace MD.Data.Mapping.Demo
{
    public partial class DemoMap : MDEntityTypeConfiguration<MD.Core.DomainModel.Demo>
    {
        public DemoMap()
        {
            this.ToTable("TDemo");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Name).IsRequired();
        }
    }
}
