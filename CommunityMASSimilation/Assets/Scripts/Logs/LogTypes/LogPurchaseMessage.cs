using Agents;
using Logs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logs
{
    public class LogPurchaseMessage : LogMessage
    {
        public Agent Seller { get; set; }
        public Resource Resource { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"Runda {Round}: {Agent.AgentName} kupił {Resource.resName} od {Seller.AgentName} za {Price}";
        }
    }
}