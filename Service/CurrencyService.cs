using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CurrencyService
    {
        public async Task<Response> ConvertCurrency(CurrencyToConvert currencyToConvert)
        {
            Response response;
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await
                client.GetAsync($"https://www.amdoren.com/api/currency.php?api_key=" +
                $"{currencyToConvert.Key}&from={currencyToConvert.FromCurrency}&to={currencyToConvert.ToCurrency}" +
                $"amount={currencyToConvert.Amount}");

                responseMessage.EnsureSuccessStatusCode();

                string message = await responseMessage.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<Response>(message);
            }
            return response;
        }
    }
}
