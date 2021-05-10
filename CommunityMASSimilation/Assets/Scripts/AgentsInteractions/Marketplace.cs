using Agents;
using Agents.Trade;
using Logs;
using System;
using System.Collections.Generic;

namespace Market
{
    public class Marketplace : SingletonBehaviour<Marketplace>
    {
        public List<Resource> allResources;

        public List<Resource> FoodResources { get; set; } = new List<Resource>();
        public List<Resource> BasicResources { get; set; } = new List<Resource>();
        public List<Resource> LuxuryResources { get; set; } = new List<Resource>();
        public List<Resource> IngredienceResources { get; set; } = new List<Resource>();

        public Dictionary<Resource, int> lastPrice = new Dictionary<Resource, int>();

        #region resources initial
        protected override void Awake()
        {
            base.Awake();

            foreach (Resource resource in allResources)
            {
                if (resource.tag == ResourceTag.Food)
                    FoodResources.Add(resource);

                if (resource.tag == ResourceTag.BasicNecessities)
                    BasicResources.Add(resource);

                if (resource.tag == ResourceTag.Luxurious)
                    LuxuryResources.Add(resource);

                if (resource.tag == ResourceTag.Ingredience)
                    IngredienceResources.Add(resource);
            }
        }

        public List<Resource> GetResourceByTypes(ResourceTag resourceType)
        {
            switch (resourceType)
            {
                case ResourceTag.Ingredience:
                    return IngredienceResources;
                case ResourceTag.Food:
                    return FoodResources;
                case ResourceTag.BasicNecessities:
                    return BasicResources;
                case ResourceTag.Luxurious:
                    return LuxuryResources;
                default:
                    throw new InvalidOperationException(resourceType.ToString());
            }
        }

        public int? GetLastResoureceTradePrice(Resource resource)
        {
            if (lastPrice.ContainsKey(resource))
                return lastPrice[resource];
            else
                return null;
        }
        #endregion

        #region Demand & Supply

        public float GetDemand(Agent querist, Resource resource)
        {
            float demand = 0;
            foreach(Agent agent in AgentQueueController.instance.GetAliveAgents())
            {
                if (agent != querist)
                    demand += agent.DemandAndSupply.AnswerDemand(resource);
            }

            return demand;
        }

        public float GetSupply(Agent querist, Resource resource)
        {
            float supply = 0;
            foreach (Agent agent in AgentQueueController.instance.GetAliveAgents())
            {
                if (agent != querist)
                    supply += agent.DemandAndSupply.AnswerSupply(resource);
            }

            return supply;
        }

        #endregion

        #region Trade

        public List<TradeOffer> GetTradeOffers(Resource resource)
        {
            List<TradeOffer> tradeOffers = new List<TradeOffer>();

            foreach (Agent seller in AgentQueueController.instance.GetAliveAgents())
            {
                TradeOffer tradeOffer = seller.TradeController.AnswerResourceTradeOffer(resource);
                if (tradeOffer != null)
                    tradeOffers.Add(tradeOffer);
            }

            return tradeOffers;
        }

        public void Trade(Agent buyer, TradeOffer tradeOffer)
        {
            LogManager.instance.AddPurchageLog(tradeOffer.Seller, buyer, tradeOffer.Resource, tradeOffer.Price);
            LogManager.instance.AddSaleLog(tradeOffer.Seller, buyer, tradeOffer.Resource, tradeOffer.Price);

            buyer.Buy(tradeOffer.Resource, tradeOffer.Price, tradeOffer.Location);
            tradeOffer.Seller.Sell(tradeOffer.Resource, tradeOffer.Price);

            lastPrice[tradeOffer.Resource] = tradeOffer.Price;
        }

        #endregion

        #region Trade Offer

        public int AskResourceSellCount(Resource resource, Agent querist)
        {
            int count = 0;

            foreach (Agent seller in AgentQueueController.instance.GetAliveAgents())
            {
                if(seller != querist)
                {
                    count += seller.TradeController.AnswerResourceSellCount(resource);
                }
            }

            return count;
        }

        public float? AskAvgSellPrice(Resource resource, Agent querist)
        {
            float sum = 0;
            int count = 0;

            foreach (Agent seller in AgentQueueController.instance.GetAliveAgents())
            {
                if(seller != querist)
                {
                    int? price = seller.TradeController.AnswerResourceSellPrice(resource);
                    if (price.HasValue)
                    {
                        int itemCount = seller.TradeController.AnswerResourceSellCount(resource);
                        sum += price.Value * itemCount;
                        count += itemCount;
                    }
                }
            }

            if (count != 0)
                return sum/count;

            return null;
        }

        #endregion
    }
}