using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Configuration;
using MD.Core.Data;
using MD.Services.Demo;
using MD.Data;
using MD.Services.Register;

namespace MD.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
