using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Identity.Authorization
{
    /// </summary>
    [Alias("AbpUserLogins")]
    public class UserLogin : Entity<long>
    {
        /// <summary>
        /// Maximum length of <see cref="LoginProvider"/> property.
        /// </summary>
        public const int MaxLoginProviderLength = 128;

        /// <summary>
        /// Maximum length of <see cref="ProviderKey"/> property.
        /// </summary>
        public const int MaxProviderKeyLength = 256;

        /// <summary>
        /// Id of the User.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Login Provider.
        /// </summary>
        [Required]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Key in the <see cref="LoginProvider"/>.
        /// </summary>
        [Required]
        public virtual string ProviderKey { get; set; }
    }
}
