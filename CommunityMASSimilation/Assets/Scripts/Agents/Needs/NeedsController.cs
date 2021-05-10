using Agents.Core;
using Agents.Trade;
using Agents.WorkplaceSelection;
using System.Collections.Generic;

namespace Agents.Needs
{
    public class NeedsController
    {
        public Dictionary<ResourceTag, NeedsConsumptionBase> NeedsConsumptionBase = new Dictionary<ResourceTag, NeedsConsumptionBase>();

        private Energy energy;

        public NeedsController(Wallet wallet, Energy energy, Inventory inventory, AgentWorkplace workplace,
            DemandAndSupply demandAndSupply, EnergyConsumptionCost energyConsumptionCost,
            ResourcesPricesEstimation resourcesPricesEstimation, TradeController tradeController)
        {
            this.energy = energy;


            NeedsConsumptionBase[ResourceTag.Food] = new FoodConsumption(wallet, energy, inventory, workplace, demandAndSupply, energyConsumptionCost, resourcesPricesEstimation, tradeController);
            NeedsConsumptionBase[ResourceTag.BasicNecessities] = new BasicNeedsConsumption(wallet, energy, inventory, workplace, demandAndSupply, energyConsumptionCost, resourcesPricesEstimation, tradeController);
            NeedsConsumptionBase[ResourceTag.Luxurious] = new LuxuriousNeedsConsumption(wallet, energy, inventory, workplace, demandAndSupply, energyConsumptionCost, resourcesPricesEstimation, tradeController);
        }

        public void UpdateConsumption()
        {
            NeedsConsumptionBase[ResourceTag.Food].UpdateConsumption();
            NeedsConsumptionBase[ResourceTag.BasicNecessities].UpdateConsumption();
            NeedsConsumptionBase[ResourceTag.Luxurious].UpdateConsumption();

            ConsumeEnergy();
        }

        public void ConsumeEnergy()
        {
            energy.ConsumeEnergy(EnergyConsumptionCalculator.CalculateEnergyReducingValue(energy.CalculateSatisfaction()));
        }
    }
}
