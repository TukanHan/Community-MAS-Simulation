using Agents.Trade;
using Market;
using System;
using System.Collections.Generic;

namespace Agents.Core
{
    public class DemandAndSupply
    {
        private Inventory inventory;

        private Dictionary<Resource, float> Supply = new Dictionary<Resource, float>();
        private Dictionary<Resource, float> Demand = new Dictionary<Resource, float>();

        public DemandAndSupply(Inventory inventory)
        {
            this.inventory = inventory;

            foreach (Resource resource in Marketplace.instance.allResources)
            {
                Supply.Add(resource, 0);
                Demand.Add(resource, 0);
            }
        }

        public void SetDemand(Resource resource, float demand)
        {
            Demand[resource] = demand;
        }

        public void SetSupply(Resource resource, float supply)
        {
            Supply[resource] = supply;
        }

        public float AnswerDemand(Resource resource)
        {
            return Math.Max(0, Demand[resource] - inventory.GetResourceCount(resource));
        }

        public float AnswerSupply(Resource resource)
        {
            return Supply[resource];
        }

        public IEnumerable<Resource> AnswerDemands()
        {
            foreach(var key in Demand.Keys)
            {
                if (Demand[key] > 0)
                    yield return key;
            }
        }
    }
}
