using Agents.Core;
using Agents.Trade;
using Agents.WorkplaceSelection;

namespace Agents.Needs
{
    public class LuxuriousNeedsConsumption : NeedsConsumptionBase
    {
        protected override ResourceTag NeedsType { get { return ResourceTag.Luxurious; } }
        protected override float EnergyConsumptionFactor { get { return Energy.LuxuryConsumptionFactor; } }
        protected override float EnergyConsumptionPriority { get { return Energy.LuxuryConsumptionPriority; } }

        public LuxuriousNeedsConsumption(Wallet wallet, Energy energy, Inventory inventory,
            AgentWorkplace workplace, DemandAndSupply demandAndSupply, EnergyConsumptionCost energyConsumptionCost,
            ResourcesPricesEstimation resourcesPricesEstimation, TradeController tradeController)
            : base(wallet, energy, inventory, workplace, demandAndSupply, energyConsumptionCost,
                  resourcesPricesEstimation, tradeController)
        {
        }

        protected override float GetNeedsValue()
        {
            return energy.LuxuryNeeds;
        }

        protected override void AddEnergy(float value)
        {
            energy.AddLuxuryEnergy(value);
        }

        protected override int CalculateSubjectiveEnergyPrice()
        {
            float priority = (1 - GetNeedsValue()) / 3;
            return (int)(priority * wallet.GetCurrency());
        }

        protected override bool AreLowerNeedsMeeted()
        {
            return energy.AreBasicNeedsMeeting;
        }
    }
}