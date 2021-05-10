using Agents;

namespace Logs
{
    public class LogSaleMessage : LogMessage
    {
        public Agent Buyer { get; set; }
        public Resource Resource { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"Runda {Round}: {Agent.AgentName} sprzedał {Resource.resName} agentowi {Buyer.AgentName} za {Price}";
        }
    }
}