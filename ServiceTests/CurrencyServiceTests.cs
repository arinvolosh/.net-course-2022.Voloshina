using Xunit;
using Services;
using Models;

namespace ServiceTests
{
    [Fact]
    public async Task ConvertCurrencyTest()
    {
        //Arrange
        CurrencyService currencyService = new CurrencyService();
        var сurrencyToConvert = new CurrencyToConvert
        {
            Key = "z3YizVAwbUzy7WAV4MHNDC9r4n9fUj",
            FromCurrency = "USD",
            ToCurrency = "RUB",
            Amount = 100
        };
        //Act
        var response = await currencyService.ConvertCurrency(сurrencyToConvert);
        //Assert
        Assert.Equal("0", response.Error);
    }
}
