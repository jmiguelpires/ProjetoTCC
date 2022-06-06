using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCC.API
{
    public class Servico
    {
        private static string UrlBase { get; set; }

        //LOCALHOST
        //private const string _urlBase = "https://localhost:44337/api/Usuario/";
        //private const string _urlBase = "https://localhost:44337/api/";
        //private const string _urlBase = "http://127.0.0.1:25486/api/";
        private const string _urlBase = "http://10.0.2.2:25486/api/";

        private const int maximoTentativas = 1;

        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(40);

        public Servico()
        {
            //sempre começa na url principal
            if (string.IsNullOrEmpty(UrlBase))
            {
                UrlBase = _urlBase;
            }
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        private void RequestHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            //string bearer = "Bearer " + Util.Global.UsuarioGlobal?.Token ?? "";
            //string bearer = "Bearer " + "";
            //client.DefaultRequestHeaders.Add("Authorization", bearer);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> PostAsync<T>(string url, string json)
        {
            int contador = 1;

            do
            {
                try
                {
                    using (var client = new HttpClient(GetInsecureHandler()))
                    {
                        client.BaseAddress = new Uri(UrlBase);
                        RequestHeader(client);
                        client.Timeout = _timeout;

                        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(UrlBase + url, stringContent);

                        string jsonResult = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            var result = JsonConvert.DeserializeObject<T>(json);
                            return result;
                        }
                        //se for 408(timeout), 404(notfound), 208 entre 500 e 599, tenta de novo
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.RequestTimeout || (int)response.StatusCode == 208 || ((int)response.StatusCode >= 500 && (int)response.StatusCode <= 599))
                        {
                            contador += 1;

                            //se for 500 é erro de PRIMARY KEY, CPF/CNPJ que está tentando ser cadastrado já consta na base de dados
                            if (((int)response.StatusCode == 500))
                            {
                                throw new Exception("Este CPF/CNPJ já é cadastrado em nosso sistema.");
                            }
                        }
                        else
                        {
                            try
                            {
                                var messageError = JsonConvert.DeserializeObject<MessageError>(jsonResult);
                                throw new Exception(messageError.Message);
                            }
                            catch (JsonException)
                            {
                                throw new Exception(jsonResult);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                }
                // Caso tenha acontecido alguma WebException, devemos trocar a URL e tentar novamente.
                catch (System.Net.WebException e)
                {
                    contador += 1;
                    if (e.Response is System.Net.HttpWebResponse)
                    {
                        System.Net.HttpWebResponse Resp = (System.Net.HttpWebResponse)e.Response;

                        // Se não for 208 ou 400 troca a URL
                        if (((int)Resp.StatusCode != 208))
                        {

                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Verifique sua conexão com a internet: " + e.Message);
                }
                catch (TaskCanceledException e)
                {
                    contador += 1;

                    throw new Exception("Tempo limite esgotado, verifique sua conexão com a internet: " + e.Message);
                }
                catch (OperationCanceledException)
                {
                    contador += 1;

                }
                catch (Exception e)
                {
                    throw e;
                }

            } while (contador <= 1);

            throw new Exception("Quantidade máximas de tentativas esgotadas, verifique sua conexão com a internet.");
        }

        public async Task<T> GetUsuarioAsync<T>(string url, string json)
        {
            int contador = 1;

            do
            {
                try
                {
                    using (var client = new HttpClient(GetInsecureHandler()))
                    {
                        client.BaseAddress = new Uri(UrlBase);
                        RequestHeader(client);
                        client.Timeout = _timeout;

                        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await client.GetAsync(UrlBase + url);

                        string jsonResult = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            var result = JsonConvert.DeserializeObject<T>(jsonResult);
                            return result;
                        }
                        //se for 408(timeout), 404(notfound), 208 entre 500 e 599, tenta de novo
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.RequestTimeout || (int)response.StatusCode == 208 || ((int)response.StatusCode >= 500 && (int)response.StatusCode <= 599))
                        {
                            contador += 1;

                            //se for 500 é erro de PRIMARY KEY, CPF/CNPJ que está tentando ser cadastrado já consta na base de dados
                            if (((int)response.StatusCode == 500))
                            {
                                throw new Exception("Este CPF/CNPJ já é cadastrado em nosso sistema.");
                            }
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            throw new Exception("Usuário não cadastrado ou senha inválida!");
                        }
                        else
                        {
                            try
                            {
                                var messageError = JsonConvert.DeserializeObject<MessageError>(jsonResult);
                                throw new Exception(messageError.Message);
                            }
                            catch (JsonException)
                            {
                                throw new Exception(jsonResult);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                }
                // Caso tenha acontecido alguma WebException, devemos trocar a URL e tentar novamente.
                catch (System.Net.WebException e)
                {
                    contador += 1;
                    if (e.Response is System.Net.HttpWebResponse)
                    {
                        System.Net.HttpWebResponse Resp = (System.Net.HttpWebResponse)e.Response;

                        // Se não for 208 ou 400 troca a URL
                        if (((int)Resp.StatusCode != 208))
                        {

                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Verifique sua conexão com a internet: " + e.Message);
                }
                catch (TaskCanceledException e)
                {
                    contador += 1;

                    throw new Exception("Tempo limite esgotado, verifique sua conexão com a internet: " + e.Message);
                }
                catch (OperationCanceledException)
                {
                    contador += 1;

                }
                catch (Exception e)
                {
                    throw e;
                }

            } while (contador <= 1);

            throw new Exception("Quantidade máximas de tentativas esgotadas, verifique sua conexão com a internet.");
        }

        public async Task<List<T>> GetAsync<T>(string url)
        {
            int contador = 1;

            do
            {
                try
                {
                    using (var client = new HttpClient(GetInsecureHandler()))
                    {
                        client.BaseAddress = new Uri(UrlBase);
                        //RequestHeader(client);
                        client.Timeout = _timeout;

                        var response = await client.GetAsync(UrlBase + url);

                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            //await Usuario.BuscarTokenPostAsync();

                            RequestHeader(client);

                            response = await client.GetAsync(url);
                        }

                        var jsonResult = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var lista = JsonConvert.DeserializeObject<List<T>>(jsonResult);
                            return lista;
                        }
                        //se for 408(timeout), 404(notfound), 208 ou entre 500 e 599, tenta de novo
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.RequestTimeout || (int)response.StatusCode == 208 || ((int)response.StatusCode >= 500 && (int)response.StatusCode <= 599))
                        {
                            contador += 1;

                            // Se não for 208 ou 400 troca a URL
                            if (((int)response.StatusCode != 208))
                            {
                                //AlteraUrl();
                            }
                        }
                        else
                        {
                            try
                            {
                                var messageError = JsonConvert.DeserializeObject<MessageError>(jsonResult);
                                var exc = new Exception(messageError.Message);

                                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //se veio badrequest, a mensagem foi enviada pela api para ser mostrada ao cliente
                                {
                                    //exc.Source = "Solidcon_API";
                                }

                                throw exc;
                            }
                            catch (JsonException)
                            {
                                throw new Exception(jsonResult);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                }
                // Caso tenha acontecido alguma WebException, devemos trocar a URL e tentar novamente.
                catch (System.Net.WebException e)
                {
                    contador += 1;
                    if (e.Response is System.Net.HttpWebResponse)
                    {
                        System.Net.HttpWebResponse Resp = (System.Net.HttpWebResponse)e.Response;

                        // Se não for 208 troca a URL
                        if (((int)Resp.StatusCode != 208))
                        {
                            //AlteraUrl();
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Verifique sua conexão com a internet: " + e.Message);
                }
                catch (TaskCanceledException e)
                {
                    contador += 1;
                    //AlteraUrl();

                    throw new Exception("Tempo limite esgotado, verifique sua conexão com a internet: " + e.Message);
                }
                catch (OperationCanceledException)
                {
                    contador += 1;
                    //AlteraUrl();
                }
                catch (Exception e)
                {
                    //throw e;
                    throw new Exception("Erro: " + e.Message);
                }

            } while (contador <= maximoTentativas);

            if (true) //(contador > maximoTentativas)
            {
                throw new Exception("Quantidade máximas de tentativas esgotadas, verifique sua conexão com a internet.");
            }
        }

        public async Task<T> GetItemAsync<T>(string url)
        {
            int contador = 1;

            do
            {
                try
                {
                    using (var client = new HttpClient(GetInsecureHandler()))
                    {
                        client.BaseAddress = new Uri(UrlBase);
                        //RequestHeader(client);
                        client.Timeout = _timeout;

                        var response = await client.GetAsync(UrlBase + url);

                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            //await Usuario.BuscarTokenPostAsync();

                            RequestHeader(client);

                            response = await client.GetAsync(url);
                        }

                        var jsonResult = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var item = JsonConvert.DeserializeObject<T>(jsonResult);
                            return item;
                        }
                        //se for 408(timeout), 404(notfound), 208 ou entre 500 e 599, tenta de novo
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.RequestTimeout || (int)response.StatusCode == 208 || ((int)response.StatusCode >= 500 && (int)response.StatusCode <= 599))
                        {
                            contador += 1;

                            // Se não for 208 ou 400 troca a URL
                            if (((int)response.StatusCode != 208))
                            {
                                //AlteraUrl();
                            }
                        }
                        else
                        {
                            try
                            {
                                var messageError = JsonConvert.DeserializeObject<MessageError>(jsonResult);
                                var exc = new Exception(messageError.Message);

                                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //se veio badrequest, a mensagem foi enviada pela api para ser mostrada ao cliente
                                {
                                    //exc.Source = "Solidcon_API";
                                }

                                throw exc;
                            }
                            catch (JsonException)
                            {
                                throw new Exception(jsonResult);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                }
                // Caso tenha acontecido alguma WebException, devemos trocar a URL e tentar novamente.
                catch (System.Net.WebException e)
                {
                    contador += 1;
                    if (e.Response is System.Net.HttpWebResponse)
                    {
                        System.Net.HttpWebResponse Resp = (System.Net.HttpWebResponse)e.Response;

                        // Se não for 208 troca a URL
                        if (((int)Resp.StatusCode != 208))
                        {
                            //AlteraUrl();
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Verifique sua conexão com a internet: " + e.Message);
                }
                catch (TaskCanceledException e)
                {
                    contador += 1;
                    //AlteraUrl();

                    throw new Exception("Tempo limite esgotado, verifique sua conexão com a internet: " + e.Message);
                }
                catch (OperationCanceledException)
                {
                    contador += 1;
                    //AlteraUrl();
                }
                catch (Exception e)
                {
                    //throw e;
                    throw new Exception("Erro: " + e.Message);
                }

            } while (contador <= maximoTentativas);

            if (true) //(contador > maximoTentativas)
            {
                throw new Exception("Quantidade máximas de tentativas esgotadas, verifique sua conexão com a internet.");
            }
        }

        public async Task<List<T>> GetAsyncIBGE<T>(string url)
        {
            try
            {
                using (var client = new HttpClient(GetInsecureHandler()))
                {
                    client.Timeout = TimeSpan.FromSeconds(15);

                    var response = await client.GetAsync(url);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //Variavel para guardar o json da resposta 
                        string jsonResult;

                        try
                        {
                            jsonResult = DescompactaGzip(await response.Content.ReadAsByteArrayAsync());
                        }
                        catch (Exception)
                        {
                            jsonResult = await response.Content.ReadAsStringAsync();
                        }

                        var item = JsonConvert.DeserializeObject<List<T>>(jsonResult);
                        return item;
                    }
                    else //if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new Exception("Serviço não encontrado: " + url);
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
            catch (Exception e)
            {
                throw e;
            }
        }

        //public async Task<List<T>> GetAsyncIBGE<T>(string url)
        //{
        //    int contador = 1;

        //    do
        //    {
        //        try
        //        {
        //            using (var client = new HttpClient())
        //            {
        //                client.BaseAddress = new Uri(UrlBase);
        //                //RequestHeader(client);
        //                client.Timeout = _timeout;

        //                var response = await client.GetAsync(url);

        //                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //                {
        //                    //await Usuario.BuscarTokenPostAsync();

        //                    //RequestHeader(client);

        //                    response = await client.GetAsync(url);
        //                }

        //                var jsonResult = await response.Content.ReadAsStringAsync();

        //                if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //                {
        //                    var lista = JsonConvert.DeserializeObject<List<T>>(jsonResult);
        //                    return lista;
        //                }
        //                //se for 408(timeout), 404(notfound), 208 ou entre 500 e 599, tenta de novo
        //                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.RequestTimeout || (int)response.StatusCode == 208 || ((int)response.StatusCode >= 500 && (int)response.StatusCode <= 599))
        //                {
        //                    contador += 1;

        //                    // Se não for 208 ou 400 troca a URL
        //                    if (((int)response.StatusCode != 208))
        //                    {
        //                        //AlteraUrl();
        //                    }
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        var messageError = JsonConvert.DeserializeObject<MessageError>(jsonResult);
        //                        var exc = new Exception(messageError.Message);

        //                        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //se veio badrequest, a mensagem foi enviada pela api para ser mostrada ao cliente
        //                        {
        //                            //exc.Source = "Solidcon_API";
        //                        }

        //                        throw exc;
        //                    }
        //                    catch (JsonException)
        //                    {
        //                        throw new Exception(jsonResult);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        throw e;
        //                    }
        //                }
        //            }
        //        }
        //        // Caso tenha acontecido alguma WebException, devemos trocar a URL e tentar novamente.
        //        catch (System.Net.WebException e)
        //        {
        //            contador += 1;
        //            if (e.Response is System.Net.HttpWebResponse)
        //            {
        //                System.Net.HttpWebResponse Resp = (System.Net.HttpWebResponse)e.Response;

        //                // Se não for 208 troca a URL
        //                if (((int)Resp.StatusCode != 208))
        //                {
        //                    //AlteraUrl();
        //                }
        //            }
        //        }
        //        catch (HttpRequestException e)
        //        {
        //            throw new Exception("Verifique sua conexão com a internet: " + e.Message);
        //        }
        //        catch (TaskCanceledException)
        //        {
        //            contador += 1;
        //            //AlteraUrl();

        //            //throw new Exception("Tempo limite esgotado, verifique sua conexão com a internet: " + e.Message);
        //        }
        //        catch (OperationCanceledException)
        //        {
        //            contador += 1;
        //            //AlteraUrl();
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }

        //    } while (contador <= maximoTentativas);

        //    if (true) //(contador > maximoTentativas)
        //    {
        //        throw new Exception("Quantidade máximas de tentativas esgotadas, verifique sua conexão com a internet.");
        //    }
        //}

        public async Task DeleteAsync(string url)
        {
            int contador = 1;

            do
            {
                try
                {
                    using (var client = new HttpClient(GetInsecureHandler()))
                    {
                        client.BaseAddress = new Uri(UrlBase);
                        //RequestHeader(client);
                        client.Timeout = _timeout;

                        var response = await client.DeleteAsync(UrlBase + url);

                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            //await Usuario.BuscarTokenPostAsync();

                            RequestHeader(client);

                            response = await client.DeleteAsync(url);
                        }

                        string jsonResult = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            return;
                        }
                        //se for 408(timeout), 404(notfound), 208 ou entre 500 e 599, tenta de novo
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.RequestTimeout || (int)response.StatusCode == 208 || ((int)response.StatusCode >= 500 && (int)response.StatusCode <= 599))
                        {
                            contador += 1;

                            // Se não for 208 ou 400 troca a URL
                            if (((int)response.StatusCode != 208))
                            {
                               // AlteraUrl();
                            }
                        }
                        else
                        {
                            try
                            {
                                var messageError = JsonConvert.DeserializeObject<MessageError>(jsonResult);
                                throw new Exception(messageError.Message);
                            }
                            catch (JsonException)
                            {
                                throw new Exception(jsonResult);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                }
                // Caso tenha acontecido alguma WebException, devemos trocar a URL e tentar novamente.
                catch (System.Net.WebException e)
                {
                    contador += 1;
                    if (e.Response is System.Net.HttpWebResponse)
                    {
                        System.Net.HttpWebResponse Resp = (System.Net.HttpWebResponse)e.Response;

                        // Se não for 208 ou 400 troca a URL
                        if (((int)Resp.StatusCode != 208))
                        {
                            //AlteraUrl();
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Verifique sua conexão com a internet: " + e.Message);
                }
                catch (TaskCanceledException)
                {
                    contador += 1;
                    //AlteraUrl();

                    //throw new Exception("Tempo limite esgotado, verifique sua conexão com a internet: " + e.Message);
                }
                catch (OperationCanceledException)
                {
                    contador += 1;
                    //AlteraUrl();
                }
                catch (Exception e)
                {
                    throw e;
                }

            } while (contador <= maximoTentativas);

            if (true) //(contador > maximoTentativas)
            {
                throw new Exception("Quantidade máximas de tentativas esgotadas, verifique sua conexão com a internet.");
            }
        }

        public async Task<T> PutAsync<T>(string url, string json)
        {
            int contador = 1;

            do
            {
                try
                {
                    using (var client = new HttpClient(GetInsecureHandler()))
                    {
                        client.BaseAddress = new Uri(UrlBase);
                        RequestHeader(client);
                        client.Timeout = _timeout;
                        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await client.PutAsync(UrlBase + url, stringContent);

                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            //await Usuario.BuscarTokenPostAsync();

                            //RequestHeader(client);

                            //response = await client.GetAsync(url);
                            response = await client.PutAsync(url, stringContent);
                        }

                        string jsonResult = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            var result = JsonConvert.DeserializeObject<T>(jsonResult);

                            return result;
                        }
                        //se for 408(timeout), 404(notfound), 208 ou entre 500 e 599, tenta de novo
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.RequestTimeout || (int)response.StatusCode == 208 || ((int)response.StatusCode >= 500 && (int)response.StatusCode <= 599))
                        {
                            contador += 1;

                            // Se não for 208 ou 400 troca a URL
                            if (((int)response.StatusCode != 208))
                            {
                                //AlteraUrl();
                            }
                        }
                        else
                        {
                            try
                            {
                                var messageError = JsonConvert.DeserializeObject<MessageError>(jsonResult);
                                throw new Exception(messageError.Message);
                            }
                            catch (JsonException)
                            {
                                throw new Exception(jsonResult);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                }
                // Caso tenha acontecido alguma WebException, devemos trocar a URL e tentar novamente.
                catch (System.Net.WebException e)
                {
                    contador += 1;
                    if (e.Response is System.Net.HttpWebResponse)
                    {
                        System.Net.HttpWebResponse Resp = (System.Net.HttpWebResponse)e.Response;

                        // Se não for 208 ou 400 troca a URL
                        if (((int)Resp.StatusCode != 208))
                        {
                            //AlteraUrl();
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Verifique sua conexão com a internet: " + e.Message);
                }
                catch (TaskCanceledException)
                {
                    contador += 1;
                    //AlteraUrl();

                    //throw new Exception("Tempo limite esgotado, verifique sua conexão com a internet: " + e.Message);
                }
                catch (OperationCanceledException)
                {
                    contador += 1;
                    //AlteraUrl();
                }
                catch (Exception e)
                {
                    throw e;
                }

            } while (contador <= maximoTentativas);

            if (true) //(contador > maximoTentativas)
            {
                throw new Exception("Quantidade máximas de tentativas esgotadas, verifique sua conexão com a internet.");
            }
        }

        public static string DescompactaGzip(byte[] origem)
        {
            string txt = null;
            System.IO.MemoryStream _ms = new System.IO.MemoryStream();
            System.IO.Compression.GZipStream _zipStream = null;

            try
            {
                byte[] _buffer = origem; //= Convert.FromBase64String(docZIP);
                int msgLength = BitConverter.ToInt32(_buffer, 0);
                _ms.Write(_buffer, 0, _buffer.Length);
                byte[] _bytes = new byte[msgLength - 1 + 1];
                _ms.Position = 0;
                _zipStream = new System.IO.Compression.GZipStream(_ms, System.IO.Compression.CompressionMode.Decompress);
                _zipStream.Read(_bytes, 0, _bytes.Length);
                txt = System.Text.Encoding.UTF8.GetString(_bytes);
                //_xml = _xml.Replace(Constants.vbNullChar, null);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _ms.Close();
                _zipStream.Close();
            }
            return txt;
        }

        private class MessageError
        {
            public string Message { get; set; }
        }
    }


}
