using LibMain.Configuration.Startup;
using LibMain.Dependency;
using LibMain.Dependency.Installer;
using LibMain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain
{
    public class Bootstrapper:IDisposable
    {
        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private IModuleManager _moduleManager;

        /// <summary>
        /// Creates a new <see cref="Bootstrapper"/> instance.
        /// </summary>
        public Bootstrapper()
            : this(Dependency.IocManager.Instance)
        {

        }

        /// <summary>
        /// Creates a new <see cref="Bootstrapper"/> instance.
        /// </summary>
        /// <param name="iocManager">IIocManager that is used to bootstrap the ABP system</param>
        public Bootstrapper(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        /// <summary>
        /// Initializes the ABP system.
        /// </summary>
        public virtual void Initialize()
        {
            IocManager.IocContainer.Install(new CoreInstaller());

            IocManager.Resolve<StartupConfiguration>().Initialize();

            _moduleManager = IocManager.Resolve<IModuleManager>();
            _moduleManager.InitializeModules();
        }

        /// <summary>
        /// Disposes the ABP system.
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (_moduleManager != null)
            {
                _moduleManager.ShutdownModules();
            }
        }
    }
}
