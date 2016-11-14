using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Identity.Users
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    
    public class User2 : Entity<int>
    {
        /// <summary>
        /// Tenant Id of this user.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name { get; set; }



        /// <summary>
        /// User name.
        /// User name must be unique for it's tenant.
        /// </summary>
        public string Value { get; set; }

        public bool IsActive { get; set; }

        public override string ToString()
        {
            return string.Format("[User {0}]{1}", Id, Name);
        }
    }
}
