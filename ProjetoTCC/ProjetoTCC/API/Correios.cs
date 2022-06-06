using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ProjetoTCC.API;

namespace ProjetoTCC.API
{
    public class Correios
    {
        public static async Task<List<Model.Municipio>> MunicipiosGetAsync(int cdEstado)
        {
            try
            {
                //var url = $"ibge/estado/{cdEstado}/GetMunicipios";
                var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{cdEstado}/municipios";

                var servico = new Servico();
                var result = await servico.GetAsyncIBGE<Model.Municipio>(url);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static async Task<Model.Correio> CorreioGetAsync(string Cep)
        {
            try
            {
                var url = $"https://api.postmon.com.br/v1/cep/{Cep.PadLeft(8, '0')}";

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);

                    var response = await client.GetAsync(url);

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound) //alguns ceps aparentemente retornam 404 na chamada https, tenta http
                    {
                        response = await client.GetAsync(url.Replace("https://", "http://"));
                    }

                    var jsonResult = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var item = JsonConvert.DeserializeObject<Model.Correio>(jsonResult);
                        return item;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Verifique sua conexão com a internet: " + e.Message);
            }
            catch (TaskCanceledException e)
            {
                throw new Exception("Tempo limite esgotado, verifique sua conexão com a internet: " + e.Message);
            }
            catch
            {
                throw new Exception("Cep não encontrado!");
            }
        }
    }

}
