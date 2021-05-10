using Agents.Core;
using Agents.Trade;
using Agents.WorkplaceSelection;

namespace Agents.Needs
{
    public class BasicNeedsConsumption : NeedsConsumptionBase
    {
        protected override ResourceTag NeedsType { get { return ResourceTag.BasicNecessities; } }
        protected override float EnergyConsumptionFactor { get { return Energy.BaseConsumptionFactor; } }
        protected override float EnergyConsumptionPriority { get { return Energy.BaseConsumptionPriority; } }

        public BasicNeedsConsumption(Wallet wallet, Energy energy, Inventory inventory,
            AgentWorkplace workplace, DemandAndSupply demandAndSupply, EnergyConsumptionCost energyConsumptionCost,
            ResourcesPricesEstimation resourcesPricesEstimation, TradeController tradeController) 
            : base(wallet, energy, inventory, workplace, demandAndSupply, energyConsumptionCost,
                  resourcesPricesEstimation, tradeController)
        {
        }

        protected override float GetNeedsValue()
        {
            return energy.BasicNeeds;
        }

        protected override void AddEnergy(float value)
        {
            energy.AddBasicEnergy(value);
        }

        protected override int CalculateSubjectiveEnergyPrice()
        {
            float priority = (1 - GetNeedsValue()) / 2;
            return (int)(priority * wallet.GetCurrency());
        }

        protected override bool AreLowerNeedsMeeted()
        {
            return energy.AreFoodNeedsMeeting;
        }
    }
}