using ClienteWpf.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ClienteWpf.ProvaRemotaRequests
{
    public static class ProvaRemotaApiRequests
    {
        private static readonly string apiBasicUri = "http://localhost:5073/ClienteCapta"; // Porta API

        public static async Task<List<ClienteDto>> ObterClientes()
        {
            var clientes = new List<ClienteDto>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(apiBasicUri + "/GetClienteCapta"))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != null)
                            {
                                return JsonConvert.DeserializeObject<List<ClienteDto>>(data);

                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                
            }
            return clientes;
        }

        public static async Task<List<TipoClienteDto>> ObterTipos()
        {
            var tipos = new List<TipoClienteDto>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(apiBasicUri + "/GetTiposCapta"))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != null)
                            {
                                return JsonConvert.DeserializeObject<List<TipoClienteDto>>(data);

                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {

            }
            return tipos;
        }
        public static async Task<List<SituacaoDto>> ObterSituacoes()
        {
            var situacoes = new List<SituacaoDto>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(apiBasicUri + "/GetSituacoesCapta"))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != null)
                            {
                                return JsonConvert.DeserializeObject<List<SituacaoDto>>(data);

                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {

            }
            return situacoes;
        }

        public static async Task<bool> DeletarCliente(int idCliente)
        {
            bool deletou = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.DeleteAsync(apiBasicUri + "/" + idCliente).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        deletou = true;
                    }
                }
            }
            catch (Exception exception)
            {
                deletou = true;
            }
            return deletou;
        }

        public static async Task<ClienteDto> AdicionaClienteNovo(ClienteDto cliente)
        {
            var clienteRetorno = new ClienteDto();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var clienteJson = JsonConvert.SerializeObject(cliente);
                    var stringContent = new StringContent(clienteJson, UnicodeEncoding.UTF8, "application/json");
                    using (HttpResponseMessage res = await client.PostAsync(apiBasicUri + "/AdicionaClienteCapta", stringContent))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            if (!res.IsSuccessStatusCode)
                            {
                                return null;
                            }
                            if (data != null)
                            {
                                return JsonConvert.DeserializeObject<ClienteDto>(data);

                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {

            }
            return cliente;
        }
        public static async Task<bool> AlteraCliente(int idCliente, UpdateClienteDto updateCliente)
        {
            var valido = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var clienteJson = JsonConvert.SerializeObject(updateCliente);
                    var stringContent = new StringContent(clienteJson, UnicodeEncoding.UTF8, "application/json");
                    using (HttpResponseMessage res = await client.PutAsync(apiBasicUri + "/" + idCliente, stringContent).ConfigureAwait(false))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            valido = res.IsSuccessStatusCode;
 
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                valido = false;
            }
            return valido;
        }
    }
}
