using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobet.Dependency;
namespace Mobet.Events.Configuration
{
    /// <summary>
    /// Used to configure <see cref="IEventBus"/>.
    /// </summary>
    public interface IEventBusConfiguration : ISingletonDependency
    {
        /// <summary>
        /// True, to use <see cref="EventBus.Default"/>.
        /// False, to create per <see cref="IIocManager"/>.
        /// This is generally set to true. But, for unit tests,
        /// it can be set to false.
        /// Default: true.
        /// </summary>
        bool UseDefaultEventBus { get; set; }
    }
}
