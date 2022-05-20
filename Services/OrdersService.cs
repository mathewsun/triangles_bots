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
        public OrdersService()
        {
            
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

            DateTime currentSecond = DateTime.Now;

            _ = socketClient.Spot.SubscribeToTradeUpdatesAsync("BTCUSDT", async data =>
            {
                pair1price = data.Data.Price;

                //currentSecond = data.Data.TradeTime;
            });
            
            _ = socketClient.Spot.SubscribeToTradeUpdatesAsync("ETHUSDT", async data =>
            {
                pair2price = data.Data.Price;
            });

            _ = socketClient.Spot.SubscribeToTradeUpdatesAsync("ETHBTC", async data =>
            {
                pair3price = data.Data.Price;
            });

        }
    }
}
