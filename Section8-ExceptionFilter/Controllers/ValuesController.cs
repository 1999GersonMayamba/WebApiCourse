using Section8_ExceptionFilter.Filters;
using Section8_ExceptionFilter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Section8_ExceptionFilter.Controllers
{

    //Registrando filtros de exceção
    //Há várias maneiras de registrar um filtro de exceção da API Web:
    //1- por ação
    //2- pelo controlador
    //3- globalmente


    //Para aplicar o filtro a todas as ações em um controlador, adicione o filtro como um atributo à classe do controlador:
    [NotImplExceptionFilter]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //Para aplicar o filtro em uma ação específica, adicione o filtro como um atributo à ação:
        //[NotImplExceptionFilter] 
        // GET api/values/5
        public string Get(int id)
        {
            //HttpResponseException foi projetado especificamente para retornar uma resposta http. 
            //if (id == 0)
            //{
            //    //Aqui retona o status 404 para o cliente
            //   // throw new HttpResponseException(HttpStatusCode.NotFound);

            //    //Aqui cria o corpo de resposta como status Http
            //    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            //    {
            //        //Aqui cria o Content (Corpo de resposta)
            //        Content = new StringContent(string.Format("No product with ID = {0}", id)),
            //        //Aqui tem a mensagem de retorno que vai ficar entre o status code retornoda E.g (404 Product ID Not Found)
            //        ReasonPhrase = "Product ID Not Found"
            //    };
            //    throw new HttpResponseException(resp);
            //}


            return "value";
        }

       // HttpError
       // O objeto HttpError fornece uma maneira consistente de retornar informações de
       // erro no corpo da resposta.O exemplo a seguir mostra como retornar o código 
       //de status HTTP 404 (não encontrado) com um HttpError no corpo da resposta.

        [Route("api/Values/Range/{min}/{max}")]
        [HttpGet]
        public HttpResponseMessage GetNumeros(int min, int max)
        {
            if (min == 0 && max == 0)
            {
                var message = string.Format("Não tem como gerar o range de valores porque o min {0}, é igual ão max {1}", min, max);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
            else if(min > max)
            {
                var message = string.Format("Não tem como gerar o range de valores porque o min {0}, é maior que o max {1}", min, max);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
            else
            {
                List<int> Inteiros = new List<int>();
                for(int x = min; x <= max; x++)
                {
                    Inteiros.Add(x);
                }
                return Request.CreateResponse(HttpStatusCode.OK, Inteiros);
            }
        }


        //Usando HttpError com HttpResponseexception
        //Os exemplos anteriores retornam uma mensagem HttpResponseMessage da ação do controlador, 
        //mas você também pode usar httpresponseexception para retornar um HttpError.Isso permite que 
        //você retorne um modelo fortemente tipado no caso de êxito normal, enquanto ainda retorna 
        //HttpError se houver um erro:
        [Route("api/Values/Range2/{min}/{max}")]
        [HttpGet]
        public HttpResponseMessage GetNumeros2(int min, int max)
        {
            if (min == 0 && max == 0)
            {
                var message = string.Format("Não tem como gerar o range de valores porque o min {0}, é igual ão max {1}", min, max);
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);

                //Aqui faz se associação do HttpResponseException com CreateErrorResponse
                //Passando como parameto o CreateErrorResponse ao HttpResponseException
                // var message = string.Format("Product with id = {0} not found", id);
                throw new HttpResponseException(
                   Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            else if (min > max)
            {
                var message = string.Format("Não tem como gerar o range de valores porque o min {0}, é maior que o max {1}", min, max);
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);

                //Aqui faz se associação do HttpResponseException com CreateErrorResponse
                //Passando como parameto o CreateErrorResponse ao HttpResponseException
                // var message = string.Format("Product with id = {0} not found", id);
                throw new HttpResponseException(
                   Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            else
            {
                List<int> Inteiros = new List<int>();
                for (int x = min; x <= max; x++)
                {
                    Inteiros.Add(x);
                }
                return Request.CreateResponse(HttpStatusCode.OK, Inteiros);
            }
        }

        //HttpError e validação de modelo
        //Para a validação do modelo, você pode passar o estado do modelo para CreateErrorResponse, 
        //para incluir os erros de validação na resposta:
        // POST api/values
        [ValidateModelAttribute]
        [HttpPost]
        public HttpResponseMessage PostCliente(Tb_Cliente tb_Cliente)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Aluno cadastrado com sucesso");
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
