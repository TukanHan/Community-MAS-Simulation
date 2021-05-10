using Agents.Core;
using Agents.Trade;
using Agents.WorkplaceSelection;

namespace Agents.Needs
{
    public class FoodConsumption : NeedsConsumptionBase
    {
        protected override ResourceTag NeedsType { get { return ResourceTag.Food; } }
        protected override float EnergyConsumptionFactor { get { return Energy.FoodConsumptionFactor; } }
        protected override float EnergyConsumptionPriority { get { return Energy.FoodConsumptionPriority; } }

        public FoodConsumption(Wallet wallet, Energy energy, Inventory inventory,
            AgentWorkplace workplace, DemandAndSupply demandAndSupply, EnergyConsumptionCost energyConsumptionCost,
            ResourcesPricesEstimation resourcesPricesEstimation, TradeController tradeController)
            : base(wallet, energy, inventory, workplace, demandAndSupply, energyConsumptionCost,
                  resourcesPricesEstimation, tradeController)
        {
        }

        protected override float GetNeedsValue()
        {
            return energy.FoodNeeds;
        }

        protected override void AddEnergy(float value)
        {
            energy.AddFoodEnergy(value);
        }

        protected override int CalculateSubjectiveEnergyPrice()
        {
            float priority = 1 - GetNeedsValue();
            return (int)(priority * wallet.GetCurrency());
        }

        protected override bool AreLowerNeedsMeeted()
        {
            return true;
        }
    }
}