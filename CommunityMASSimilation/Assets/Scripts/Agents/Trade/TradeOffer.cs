using Coordinates;

namespace Agents.Trade
{
    public class TradeOffer
    {
        public Agent Seller { get; set; }
        public Resource Resource { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public CubeCoordinate Location { get; set; }
    }
}
