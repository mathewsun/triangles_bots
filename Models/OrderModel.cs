﻿namespace WebApiOnline.ApiBots.Models
{
    public class OrderModel
    {
        public string Price { get; set; }
        public string Amount { get; set; }
        public bool IsBuy { get; set; }
        public string BotAuthCode { get; set; }
        public string Pair { get; set; }
    }
}
