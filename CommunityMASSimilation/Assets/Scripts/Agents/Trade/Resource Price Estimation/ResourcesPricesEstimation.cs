using Agents.Core;
using Market;
using System.Collections.Generic;

namespace Agents.Trade
{
    public class ResourcesPricesEstimation
    {
        private Dictionary<Resource, ResourcePriceEstimation> estimations = new Dictionary<Resource, ResourcePriceEstimation>();

        public ResourcesPricesEstimation(Inventory inventory)
        {
            foreach (Resource resource in Marketplace.instance.allResources)
            {
                estimations[resource] = new ResourcePriceEstimation(() => inventory.GetResourceCount(resource));
            }
        }

        public int GetEstimatedPrice(Resource resource)
        {
            if(!estimations.ContainsKey(resource))
                throw new System.Exception("Key dont exist");

            return estimations[resource].GetPrice();
        }

        public int GetResourcesEstimationsCount()
        {
            return estimations.Count;
        }

        public IEnumerable<Resource> GetResourcesEstimations()
        {
            return estimations.Keys;
        }

        public void SetPriceAsCompetitor(Resource resource, float? avgPrice)
        {
            estimations[resource].SetPriceAsCompetitor(avgPrice);
        }

        public void SetPriceAsCustomer(Resource resource, float? avgPrice)
        {
            estimations[resource].SetPriceAsCustomer(avgPrice);
        }

        public void ResourceSold(Resource resource)
        {
            estimations[resource].ResourceSold();
        }
    }
}
