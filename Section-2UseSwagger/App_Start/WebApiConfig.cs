using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Section_2UseSwagger
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            //config.Filters.Add(new AuthorizeAttribute());
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthorizationHeaderHandler());

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
