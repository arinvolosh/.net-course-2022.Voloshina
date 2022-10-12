using Services;
using Xunit;

namespace ServiceTests
{
    public class RateUpdaterTest
    {
        [Fact]
        public void TestRateUpdater()
        {
            var rateUpdater = new RateUpdater(new ClientService(new ClientStorage()));

            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            var taskRateUpdater = rateUpdater.AccruingInterest(token);
            taskRateUpdater.Wait(20000);

            cancellationTokenSource.Cancel();
        }
    }
}
