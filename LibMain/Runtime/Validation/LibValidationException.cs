using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Runtime.Validation
{
    /// <summary>
    /// This exception type is used to throws validation exceptions.
    /// </summary>
    [Serializable]
    public class LibValidationException : AbpException
    {
        /// <summary>
        /// Detailed list of validation errors for this exception.
        /// </summary>
        public List<ValidationResult> ValidationErrors { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public LibValidationException()
        {
            ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public LibValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public LibValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationResult>();
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="validationErrors">Validation errors</param>
        public LibValidationException(string message, List<ValidationResult> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public LibValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<ValidationResult>();
        }
    }
}
