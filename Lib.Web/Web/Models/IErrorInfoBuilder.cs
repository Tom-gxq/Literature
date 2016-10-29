using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Web.Models
{
    /// <summary>
    /// This interface is used to build <see cref="ErrorInfo"/> objects.
    /// </summary>
    public interface IErrorInfoBuilder
    {
        /// <summary>
        /// Creates a new instance of <see cref="ErrorInfo"/> using the given <paramref name="exception"/> object.
        /// </summary>
        /// <param name="exception">The exception object</param>
        /// <returns>Created <see cref="ErrorInfo"/> object</returns>
        ErrorInfo BuildForException(Exception exception);

        /// <summary>
        /// Adds an <see cref="IExceptionToErrorInfoConverter"/> object.
        /// </summary>
        /// <param name="converter">Converter</param>
        void AddExceptionConverter(IExceptionToErrorInfoConverter converter);
    }
}
