using System;
using UnityEngine;

namespace Agents.Trade
{
    public class ResourcePriceEstimation
    {
        private Func<float> GetResourceCount;
        private float currentPrice;
        private int lastSoldRount = 0;
        private uint resourceUnsoldTime = 0;

        public ResourcePriceEstimation(Func<float> GetResourceCount)
        {
            this.currentPrice = AgentsPlanningPerspective.InitialPrice;
            this.GetResourceCount = GetResourceCount;
        }

        public int GetPrice()
        {
            return (int)Math.Max(1, currentPrice);
        }

        public void SetPriceAsCompetitor(float? avgPrice)
        {
            if (resourceUnsoldTime >= 2)
            {
                float change = 0.2f * GetResourceCount();
                currentPrice -= change;
                if (currentPrice < 1)
                    currentPrice = 1;
            }

            if(avgPrice.HasValue && avgPrice > currentPrice)
            {
                currentPrice = (avgPrice.Value + currentPrice) / 2f;
            }

            resourceUnsoldTime++;
        }

        public void SetPriceAsCustomer(float? avgPrice)
        {
            resourceUnsoldTime = 0;
            if (AgentQueueController.instance.Round - lastSoldRount > 3)
            {
                if (avgPrice.HasValue)
                {
                    currentPrice = (avgPrice.Value + currentPrice) / 2f;
                }
                else
                {
                    currentPrice = (AgentsPlanningPerspective.InitialPrice + currentPrice*49) / 50;
                }
            }
        }

        public void ResourceSold()
        {
            if (resourceUnsoldTime > 0)
            {
                float change = 0.2f / (GetResourceCount() + 1);
                currentPrice += change;
            }

            resourceUnsoldTime = 0;
            lastSoldRount = AgentQueueController.instance.Round;
        }
    }
}