using Agents.Core;
using Agents.Trade;
using Agents.WorkplaceSelection;
using Market;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agents.Needs
{
    public abstract class NeedsConsumptionBase
    {
        public Dictionary<Resource, float> ResourceConsumptionEnergyValue { get; } = new Dictionary<Resource, float>();

        protected const float MaxResourceEnergyValue = 0.1f;
        protected const float MinResourceEnergyValue = 0.05f;
        protected const float OvereatingValue = 0.025f;

        protected const float ResourceEnergyRegenerationValue = 0.001f;

        protected abstract float EnergyConsumptionFactor { get; }
        protected abstract float EnergyConsumptionPriority { get; }
        protected abstract ResourceTag NeedsType { get; }

        protected readonly Wallet wallet;
        protected readonly Energy energy;
        protected readonly Inventory inventory;
        protected readonly AgentWorkplace workplace;
        protected readonly DemandAndSupply demandAndSupply;
        protected readonly EnergyConsumptionCost energyConsumptionCost;
        protected readonly ResourcesPricesEstimation resourcesPricesEstimation;

        protected readonly TradeController tradeController;

        protected NeedsConsumptionBase(Wallet wallet, Energy energy, Inventory inventory, AgentWorkplace workplace,
            DemandAndSupply demandAndSupply, EnergyConsumptionCost energyConsumptionCost,
            ResourcesPricesEstimation resourcesPricesEstimation, TradeController tradeController)
        {
            this.wallet = wallet;
            this.energy = energy;
            this.inventory = inventory;
            this.workplace = workplace;
            this.demandAndSupply = demandAndSupply;
            this.energyConsumptionCost = energyConsumptionCost;
            this.resourcesPricesEstimation = resourcesPricesEstimation;
            this.tradeController = tradeController;

            foreach (Resource resource in Marketplace.instance.GetResourceByTypes(NeedsType))
            {
                ResourceConsumptionEnergyValue[resource] = MaxResourceEnergyValue;
            }
        }

        public virtual void UpdateConsumption()
        {
            IncreaseResourceEnergyRegenerationValue();

            if(AreLowerNeedsMeeted())
            {
                ConsumeResources();
                BuyNeededResources();
            }

            UpdateResourcesDemand();
        }

        protected void UpdateResourcesDemand()
        {
            var resources = Marketplace.instance.GetResourceByTypes(NeedsType);

            float resourceDemand = 0;
            if(AreLowerNeedsMeeted())
            {
                float currentEnergyLack = 1 - GetNeedsValue();
                float futureEnergyLack = AgentsPlanningPerspective.PlanningPerspective * EnergyConsumptionCalculator.CalculateEnergyReducingValue(energy.CalculateSatisfaction()) * EnergyConsumptionFactor;            

                resourceDemand = (currentEnergyLack + futureEnergyLack) / (MaxResourceEnergyValue * resources.Count) * (3 * EnergyConsumptionFactor);
            }
            
            foreach (Resource resource in resources)
            {
                demandAndSupply.SetDemand(resource, resourceDemand);
            }
        }

        protected void IncreaseResourceEnergyRegenerationValue()
        {
            foreach (var resource in Marketplace.instance.GetResourceByTypes(NeedsType))
            {
                ResourceConsumptionEnergyValue[resource] =
                    Mathf.Clamp(ResourceConsumptionEnergyValue[resource] + ResourceEnergyRegenerationValue,
                                MinResourceEnergyValue, MaxResourceEnergyValue);
            }
        }

        protected void ConsumeResource(Resource resource)
        {
            inventory.SubtractResource(resource);

            float energyValue = ResourceConsumptionEnergyValue[resource];
            AddEnergy(energyValue);

            energyConsumptionCost.AddEnergyConsumptionCost(energyValue, resourcesPricesEstimation.GetEstimatedPrice(resource));
            ResourceConsumptionEnergyValue[resource] = Math.Max(MinResourceEnergyValue, ResourceConsumptionEnergyValue[resource] - OvereatingValue);
        }

        protected void ConsumeResources()
        {
            bool succes;

            do
            {
                Resource bestResource = null;
                float rate = float.MaxValue;  //gdzie energia jest najtańsza

                foreach (var pair in ResourceConsumptionEnergyValue)
                {
                    if(inventory.HasResource(pair.Key, 1))
                    {     
                        if(resourcesPricesEstimation.GetEstimatedPrice(pair.Key) <= CalculateSubjectiveEnergyPrice() || GetNeedsValue() < 0.25)
                        {
                            float localRate = resourcesPricesEstimation.GetEstimatedPrice(pair.Key) / pair.Value;
                            if (localRate < rate)
                            {
                                bestResource = pair.Key;
                                rate = localRate;
                            }
                        }
                    }
                }

                succes = false;
                if(bestResource != null && ResourceConsumptionEnergyValue[bestResource] + GetNeedsValue() <= 1)
                {
                    succes = true;
                    ConsumeResource(bestResource);
                }

            } while (succes);
        }

        protected abstract int CalculateSubjectiveEnergyPrice();

        protected void BuyNeededResources()
        {
            List<TradeOffer> allTradeOffers = ResourceConsumptionEnergyValue.Keys
                .Where(r => !inventory.HasResource(r))
                .SelectMany(r => tradeController.AskForTradeOffers(r))
                .ToList();

            bool succes;
            do
            {
                TradeOffer bestOffer = null;
                float bestRate = float.MaxValue;

                foreach(TradeOffer offer in allTradeOffers)
                {
                    if(offer.Price <= CalculateSubjectiveEnergyPrice() && wallet.GetCurrency() >= offer.Price)
                    {
                        float costPerEnergy = offer.Price / (ResourceConsumptionEnergyValue[offer.Resource] / MaxResourceEnergyValue);

                        float travelEnegry = EnergyConsumptionCalculator.ConsumeTravelEnergy(workplace.Location, offer.Location);
                        float travelEnergyCost = energyConsumptionCost.GetCost(travelEnegry);
                        
                        float rate = costPerEnergy + travelEnergyCost;
                        if (rate < bestRate)
                        {
                            bestOffer = offer;
                            bestRate = rate;
                        }
                    }
                }

                succes = false;
                if (bestOffer != null && ResourceConsumptionEnergyValue[bestOffer.Resource] + GetNeedsValue() <= 1)
                {
                    tradeController.Trade(bestOffer);
                    if(inventory.HasResource(bestOffer.Resource,1))
                    {
                        ConsumeResource(bestOffer.Resource);
                        succes = true;
                    }

                    allTradeOffers.Remove(bestOffer);
                }

            } while (succes);
        }

        protected abstract bool AreLowerNeedsMeeted();

        protected abstract float GetNeedsValue();

        protected abstract void AddEnergy(float value);
    }
}