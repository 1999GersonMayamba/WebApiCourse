using System.Web.Http;
using WebActivatorEx;
using Section_2UseSwagger;
using Swashbuckle.Application;

/*[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]*/

namespace Section_2UseSwagger
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Section_2UseSwagger");

                    })
                .EnableSwaggerUi(c =>
                    {

                    });
        }
    }
}
