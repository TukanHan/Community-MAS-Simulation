using System;

namespace Agents.Trade
{
    public class SaleOffer
    {
        public int Price { get; set; }
        public int Count { get; set; }

        public SaleOffer() { }

        public SaleOffer(int price, int count)
        {
            Price = price;
            Count = count;
        }
    }
}