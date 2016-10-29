﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibMain
{
    /// <summary>
    /// Base exception type for those are thrown by Abp system for Abp specific exceptions.
    /// </summary>
    [Serializable]
    public class AbpException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        public AbpException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        public AbpException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public AbpException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public AbpException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
