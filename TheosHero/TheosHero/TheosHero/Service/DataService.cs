using Newtonsoft.Json;
using Polly;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheosHero.Model;

namespace TheosHero.Service
{
    public class DataService
    {
        private const string servidor = "https://gateway.marvel.com/v1/public/";
        private const string publicKey = "46ee74dbd43238ae1429181347ca6778";
        private const string privateKey = "0da869948d1316ecc4ba3147e2e464b3c0762d23";

        internal async Task<ObservableCollection<Hero>> GetHeros(string filter, int limit, int offset)
        {
            ObservableCollection<Hero> personagens = new ObservableCollection<Hero>();
            try
            {
                string ts = DateTime.Now.Ticks.ToString();
                string hash = GerarHash(ts, publicKey, privateKey);
                string filtro = GetFilter(filter, limit, offset);

                var client = new RestClient(servidor);
                var request = new RestRequest(string.Format("characters?ts={0}&apikey={1}&hash={2}{3}", ts, publicKey, hash, filtro), Method.GET);

                request.AddHeader("Content-Type", "application/json");

                await Policy
                       .Handle<HttpRequestException>(ex => !ex.Message.ToLower().Contains("404"))
                       .WaitAndRetryAsync
                       (
                           retryCount: 3,
                           sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                           onRetry: (ex, time) =>
                           {
                               Console.WriteLine($"Ocorreu um erro ao baixar os dados: {ex.Message}, tentando novamente...");
                           }
                       )
                       .ExecuteAsync(async () =>
                       {
                           var response = await client.ExecuteAsync(request);

                           var dados = JsonConvert.DeserializeObject<MarvelApiResult<Hero>>(response.Content).Data;
                           personagens = new ObservableCollection<Hero>(dados.Results);
                       });

                return await Task.Run(() => personagens);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        private string GetFilter(String filter, int limit, int offset)
        {
            var querystring = string.Empty;

            if (!string.IsNullOrWhiteSpace(filter))
                querystring += $"&nameStartsWith={System.Net.WebUtility.UrlEncode(filter)}";
            if (limit > 0)
                querystring += $"&limit={limit.ToString()}";
            if (offset > 0)
                querystring += $"&offset={offset.ToString()}";

            return querystring;
        }
        private string GerarHash(string ts, string publicKey, string privateKey)
        {
            byte[] bytes =  Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
            var gerador = MD5.Create();
            byte[] bytesHash = gerador.ComputeHash(bytes);

            return BitConverter.ToString(bytesHash).ToLower().Replace("-", String.Empty);
        }
    }
}
