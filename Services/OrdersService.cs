using Binance.Net;

using System;
using System.Threading.Tasks;

using WebApiOnline.ApiBots.Helpers;
using WebApiOnline.ApiBots.Models;
using WebApiOnline.ApiBots.Models.Constants;

namespace WebApiOnline.ApiBots.Services
{
    public class OrdersService
    {
        private TriangleRepository repo { get; set; }

        public OrdersService()
        {
            repo = new TriangleRepository();
        }

        public async Task StartSendingOrders()
        {
            Binance();
        }

        private async Task Binance()
        {
            var socketClient = new BinanceSocketClient();

            decimal pair1price = 0;
            decimal pair2price = 0;
            decimal pair3price = 0;

            DateTime currentTime = DateTime.Now;

            _ = socketClient.Spot.SubscribeToTradeUpdatesAsync("BTCUSDT", async data =>
            {
                if (data.Data.TradeTime.Second != currentTime.Second)
                {
                    repo.AddTriangleData(currentTime, "btc-eth-usdt", pair1price, pair2price, pair3price);

                    currentTime = DateTime.Parse(data.Data.TradeTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), System.Globalization.CultureInfo.CurrentCulture);
                    
                    pair1price = data.Data.Price;
                }
            });

            _ = socketClient.Spot.SubscribeToTradeUpdatesAsync("ETHUSDT", async data =>
            {
                if (data.Data.TradeTime.Second != currentTime.Second)
                {
                    repo.AddTriangleData(currentTime, "btc-eth-usdt", pair1price, pair2price, pair3price);

                    currentTime = DateTime.Parse(data.Data.TradeTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), System.Globalization.CultureInfo.CurrentCulture);

                    pair2price = data.Data.Price;
                }
            });

            _ = socketClient.Spot.SubscribeToTradeUpdatesAsync("ETHBTC", async data =>
            {
                if (data.Data.TradeTime.Second != currentTime.Second)
                {
                    repo.AddTriangleData(currentTime, "btc-eth-usdt", pair1price, pair2price, pair3price);

                    currentTime = DateTime.Parse(data.Data.TradeTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), System.Globalization.CultureInfo.CurrentCulture);

                    pair3price = data.Data.Price;
                }
            });

        }
    }
}
