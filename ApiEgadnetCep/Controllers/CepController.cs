
using ApiEgadnetCep.Authentication;
using ApiEgadnetCep.Model;
using ApiEgadnetCep.ViaCep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEgadnetCep.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController : ControllerBase
    {
        public object UserRepository { get; private set; }

        [HttpPost]
        [Authorize]
        public CepDto Post(Request request)
        {
            CepDto x = new CepDto();
            x.Origin = "Invalid Cep";
            x.Cep = "Invalid Cep";
            x.Logradouro = "Invalid Cep";
            x.Complemento = "Invalid Cep";
            x.Bairro = "Invalid Cep";
            x.Localidade = "Invalid Cep";
            x.Uf = "Invalid Cep";
            x.Ibge = "Invalid Cep";
            x.Gia = "Invalid Cep";
            x.Ddd = "Invalid Cep";
            x.Siafi = "Invalid Cep";
            if ( request.cep.Length != 8 )
            {
               return x;
            }
            else{
                foreach(char num in request.cep)
                {
                    if(num != '1' &&  num != '2' &&  num != '3' && num != '4' && num != '5' && num != '6' && num != '7' && num != '8' && num != '9' && num != '0')
                    {
                        return x;
                    }
                }
                CepDto aux = new CepDto();
                aux = ViaCepAdapter.Get(request.cep);
                if (aux.Cep != null)
                    return aux;
                else
                  return x;
            }
            
        }

        [HttpPost]
        [Route("authorize")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authorize([FromBody] User user)
        {
            if (Authenticate.Authorize(user))
            {
                var token = TokenService.GenerateToken(user);
                user.Password = "";
                
                return new
                {
                    user = user,
                    token = token
                };
            }
            return NotFound(new { message = "Usuário ou senha inválidos" });

        }
    }
}
