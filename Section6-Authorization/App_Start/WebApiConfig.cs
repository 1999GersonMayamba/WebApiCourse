using Section6_Authorization.AuthFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Section6_Authorization
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            config.Filters.Add(new AuthenticationBearerBodyApi_Key());
            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
