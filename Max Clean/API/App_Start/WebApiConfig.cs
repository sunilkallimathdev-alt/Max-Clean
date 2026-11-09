using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{id1}/{id2}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi3",
                routeTemplate: "api/{controller}/{id1}/{id2}/{id3}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi4",
                routeTemplate: "api/{controller}/{id1}/{id2}/{id3}/{id4}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "DefaultApi5",
               routeTemplate: "api/{controller}/{length}/{start}/{sortColumn}/{sortColumnDir}/{draw}/{search}",
               defaults: new { id = RouteParameter.Optional }
           );
            config.Routes.MapHttpRoute(
              name: "DefaultApi6",
              routeTemplate: "api/{controller}/{length}/{start}/{sortColumn}/{sortColumnDir}/{draw}/{search}/{id}/{FromDate}/{ToDate}",
              defaults: new { id = RouteParameter.Optional }
          );
        }
    }
}
