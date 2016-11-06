﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="ILibSession"/>.
    /// </summary>
    public static class LibSessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="AbpException"/> if <see cref="ILibSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static long GetUserId(this ILibSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new AbpException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }

        /// <summary>
        /// Gets current Tenant's Id.
        /// Throws <see cref="AbpException"/> if <see cref="ILibSession.TenantId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current Tenant's Id.</returns>
        /// <exception cref="AbpException"></exception>
        public static int GetTenantId(this ILibSession session)
        {
            if (!session.TenantId.HasValue)
            {
                throw new AbpException("Session.TenantId is null! Possible problems: User is not logged in, current user in not tenant user or this is not a multi-tenant application.");
            }

            return session.TenantId.Value;
        }
    }
}
