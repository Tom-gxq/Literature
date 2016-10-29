using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Domain.Entities
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {

    }
}
