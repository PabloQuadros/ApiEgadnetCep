using ApiEgadnetCep.Model;
using System.Net.Http;
using ApiEgadnetCep.Cache;

namespace ApiEgadnetCep.ViaCep
{
    public static class ViaCepAdapter
    {
        private static string _url = "https://viacep.com.br/ws/";

        private static HttpClient client = new HttpClient();
        public static CepDto Get(string cep)
        {
            
            CepDto product = null;
           
            foreach (CepDto x in CacheControl.CacheCep)
            {
                if (x.Cep.Replace("-", "") == cep)
                {
                    x.Origin = "Cache";
                    return x;
                }
            }
            HttpResponseMessage response =  client.GetAsync($"{_url}{cep}/json").Result;
            if (response.IsSuccessStatusCode)
            {
                product = response.Content.ReadAsAsync<CepDto>().Result;
                product.Origin = "ViaCep";
            }
            if (product.Cep != null)
            {
                CacheControl.CacheCep.Add(product);

            }
            return  product;
        }


    }
}
