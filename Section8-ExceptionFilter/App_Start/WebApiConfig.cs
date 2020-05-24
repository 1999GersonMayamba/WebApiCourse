using Section8_ExceptionFilter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Section8_ExceptionFilter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web
            //Código para iplementar filtros de excepção em toda WebAPI
            //config.Filters.Add(new NotImplExceptionFilterAttribute());


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
