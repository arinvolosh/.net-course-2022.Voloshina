using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CurrencyService
    {
        public async Task<Currency> GetCurrency(string api_key, float amount, Currency fromCurrency, Currency toCurrency)
        {
            Currency currency;
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await
                client.GetAsync($"https://www.amdoren.com/api/currency.php?api_key=" +
                $"{api_key}&from={fromCurrency.Name}&to={toCurrency}amount={amount}");

                responseMessage.EnsureSuccessStatusCode();

                string message = await responseMessage.Content.ReadAsStringAsync();

                currency = JsonConvert.DeserializeObject<Currency>(message);
            }
            return currency;
        }
    }
}
