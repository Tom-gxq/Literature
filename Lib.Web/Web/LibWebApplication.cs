using LibMain;
using LibMain.Dependency;
using LibMain.Reflection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Lib.Web
{
    public abstract class LibWebApplication : HttpApplication
    {
        /// <summary>
        /// Gets a reference to the <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        protected Bootstrapper Bootstrapper { get; private set; }

        protected LibWebApplication()
        {
            Bootstrapper = new Bootstrapper();
        }

        /// <summary>
        /// This method is called by ASP.NET system on web application's startup.
        /// </summary>
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.IocManager.RegisterIfNot<IAssemblyFinder, WebAssemblyFinder>();
            Bootstrapper.Initialize();
        }

        /// <summary>
        /// This method is called by ASP.NET system on web application shutdown.
        /// </summary>
        protected virtual void Application_End(object sender, EventArgs e)
        {
            Bootstrapper.Dispose();
        }

        /// <summary>
        /// This method is called by ASP.NET system when a session starts.
        /// </summary>
        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is called by ASP.NET system when a session ends.
        /// </summary>
        protected virtual void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is called by ASP.NET system when a request starts.
        /// </summary>
        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            var langCookie = Request.Cookies["Abp.Localization.CultureName"];
            //if (langCookie != null && GlobalizationHelper.IsValidCultureCode(langCookie.Value))
            //{
            //    Thread.CurrentThread.CurrentCulture = new CultureInfo(langCookie.Value);
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCookie.Value);
            //}
        }

        /// <summary>
        /// This method is called by ASP.NET system when a request ends.
        /// </summary>
        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //TrySetTenantId();
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Tries to set current tenant Id.
        /// </summary>
        protected virtual void TrySetTenantId()
        {
            //var claimsPrincipal = User as ClaimsPrincipal;
            //if (claimsPrincipal == null)
            //{
            //    return;
            //}

            //var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            //if (claimsIdentity == null)
            //{
            //    return;
            //}

            //var tenantIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.TenantId);
            //if (tenantIdClaim != null)
            //{
            //    return;
            //}

            //var tenantId = ResolveTenantIdOrNull();
            //if (!tenantId.HasValue)
            //{
            //    return;
            //}

            //claimsIdentity.AddClaim(new Claim(AbpClaimTypes.TenantId, tenantId.Value.ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Resolves current tenant id or returns null if can not.
        /// </summary>
        //protected virtual int? ResolveTenantIdOrNull()
        //{
            //using (var tenantIdResolver = AbpBootstrapper.IocManager.ResolveAsDisposable<ITenantIdResolver>())
            //{
            //    return tenantIdResolver.Object.TenantId;
            //}
        //}
    }
}
