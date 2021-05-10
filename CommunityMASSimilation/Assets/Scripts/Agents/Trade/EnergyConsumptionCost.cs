using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agents.Trade
{
    public class EnergyConsumptionCost
    {
        private class EnergyResourceCost
        {
            public int CompsumptionRound { get; set; }
            public float EnergyValue { get; set; }
            public float Cost { get; set; }
        }

        private Queue<EnergyResourceCost> energyResourceCostsQueue = new Queue<EnergyResourceCost>();

        private float energyValue = 0;
        private float cost = 0;

        public void Update()
        {
            while(energyResourceCostsQueue.Count > 0 && CalculateExpiryDate() >= energyResourceCostsQueue.Peek().CompsumptionRound)
            {
                EnergyResourceCost energyResourceCost = energyResourceCostsQueue.Dequeue();
                energyValue -= energyResourceCost.EnergyValue;
                cost -= energyResourceCost.Cost;
            }
        }

        private int CalculateExpiryDate()
        {
            return AgentQueueController.instance.Round - AgentsPlanningPerspective.EnergyCostPerspective;
        }

        public float GetCost(float energyValue)
        {
            float val = cost * energyValue / this.energyValue;
            return float.IsNaN(val) ? 0 : val;
        }
        
        public void AddEnergyConsumptionCost(float energyValue, float cost)
        {
            this.energyValue += energyValue;
            this.cost += cost;

            energyResourceCostsQueue.Enqueue(new EnergyResourceCost()
            {
                CompsumptionRound = AgentQueueController.instance.Round,
                Cost = cost,
                EnergyValue = energyValue
            });
        }
    }
}
