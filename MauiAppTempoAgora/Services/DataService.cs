using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "fa8c56762f23fac6ef3ff0a0036f63d6";

            // Corrige acento e espaços
            cidade = Uri.EscapeDataString(cidade);

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&lang=pt_br&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resp = await client.GetAsync(url);

                    // CIDADE NÃO ENCONTRADA
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Cidade não encontrada.");
                    }

                    // OUTROS ERROS
                    if (!resp.IsSuccessStatusCode)
                    {
                        throw new Exception($"Erro na API: {resp.StatusCode}");
                    }

                    string Json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(Json);

                    DateTime time = new();

                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new Tempo()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString("HH:mm"),
                        sunset = sunset.ToString("HH:mm"),
                    };
                }
                // ❌ SEM INTERNET
                catch (HttpRequestException)
                {
                    throw new Exception("Sem conexão com a internet.");
                }
            }

            return t;
        }
    }
}