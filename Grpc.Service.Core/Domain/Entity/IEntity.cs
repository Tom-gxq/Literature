using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Entity
{
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the entity key.
        /// </summary>
        /// <value>
        /// The entity key.
        /// </value>
        TKey Id { get; set; }
    }
}
