using Agents.Core;
using Agents.Work;
using Market;
using System.Collections.Generic;

namespace Agents.Trade
{
    public class TradeController
    {
        public Dictionary<Resource, SaleOffer> SaleAnnouncements { get; } = new Dictionary<Resource, SaleOffer>();

        readonly Agent owner;
        readonly Wallet wallet;
        readonly Inventory inventory;
        readonly AgentWorkTask currentJob;

        readonly ResourcesPricesEstimation resourcesPricesEstimation;

        public TradeController(Agent agent, Wallet wallet, Inventory inventory, AgentWorkTask currentJob,
            ResourcesPricesEstimation resourcesPricesEstimation)
        {
            owner = agent;
            this.wallet = wallet;
            this.inventory = inventory;
            this.currentJob = currentJob;
            this.resourcesPricesEstimation = resourcesPricesEstimation;

            inventory.ResourceSubstracted += ItemOutCallback;
        }

        #region Trade Anwsers
        public int? AnswerResourceSellPrice(Resource resource)
        {
            if (SaleAnnouncements.ContainsKey(resource))
                return SaleAnnouncements[resource].Price;

            return null;
        }

        public int AnswerResourceSellCount(Resource resource)
        {
            if (SaleAnnouncements.ContainsKey(resource))
                return SaleAnnouncements[resource].Count;

            return 0;
        }

        public TradeOffer AnswerResourceTradeOffer(Resource resource)
        {
            if (!SaleAnnouncements.ContainsKey(resource))
                return null;

            return new TradeOffer()
            {
                Seller = owner,
                Resource = resource,
                Price = SaleAnnouncements[resource].Price,
                Count = SaleAnnouncements[resource].Count,
                Location = owner.Workplace.Location
            };
        }

        #endregion

        #region Asks
        public int AskResourcePurchaseCount(Resource resource)
        {
            return Marketplace.instance.AskResourceSellCount(resource, owner);
        }

        public float AskResourceDemand(Resource resource)
        {
            return Marketplace.instance.GetDemand(owner, resource);
        }

        public float AskResourceSupply(Resource resource)
        {
            return Marketplace.instance.GetSupply(owner, resource);
        }

        #endregion

        #region Purchase

        public List<TradeOffer> AskForTradeOffers(Resource resource)
        {
            return Marketplace.instance.GetTradeOffers(resource);
        }

        public void Trade(TradeOffer tradeOffer)
        {
            Marketplace.instance.Trade(owner, tradeOffer);
        }

        #endregion

        #region Sale

        public void EvaluateExport()
        {
            foreach (Resource resource in Marketplace.instance.allResources)
            {
                if(SaleAnnouncements.ContainsKey(resource) && SaleAnnouncements[resource].Price == 1)
                {
                    owner.Export(resource, SaleAnnouncements[resource].Count);
                }
            }
        }

        public void EvaluateSale()
        {
            foreach (Resource resource in Marketplace.instance.allResources)
            {
                EvaluateSaleResource(resource);
            }
        }

        public void EvaluateSaleResource(Resource resource)
        {
            float? avgPrice = Marketplace.instance.AskAvgSellPrice(resource, owner);
            float resourceCount = inventory.GetResourceCount(resource);

            if (!currentJob.IsIngredience(resource) && resourceCount >= 1)
            {
                resourcesPricesEstimation.SetPriceAsCompetitor(resource, avgPrice);

                SaleAnnouncements[resource] = new SaleOffer()
                {
                    Price = resourcesPricesEstimation.GetEstimatedPrice(resource),
                    Count = (int)resourceCount
                };
            }
            else
            {
                resourcesPricesEstimation.SetPriceAsCustomer(resource, avgPrice);
                SaleAnnouncements.Remove(resource);
            }
        }

        private void ItemOutCallback(object sender, Resource resource)
        {
            if (SaleAnnouncements.ContainsKey(resource))
            {
                SaleAnnouncements[resource].Count = (int)inventory.GetResourceCount(resource);
                if (SaleAnnouncements[resource].Count == 0)
                {
                    SaleAnnouncements.Remove(resource);
                }
            }
        }

        #endregion
    }
}
