using Agents.Core;
using Agents.Needs;
using Agents.Trade;
using Agents.WorkplaceSelection;
using Professions;
using System;
using System.Collections.Generic;

namespace Agents.Work
{
    public class WorkController
    {
        public AgentTask Task { get; private set; }

        readonly Wallet wallet;
        readonly Inventory inventory;
        readonly AgentWorkTask job;
        readonly AgentWorkplace workplace;
        readonly WorkExperience workExperience;
        readonly DemandAndSupply demandAndSupply;
        readonly EnergyConsumptionCost energyConsumptionCost;
        readonly ResourcesPricesEstimation resourcesPricesEstimation;

        readonly TradeController tradeController;

        public WorkController(Wallet wallet, Inventory inventory, WorkExperience workExperience,
            AgentWorkTask job, AgentWorkplace workplace, DemandAndSupply demandAndSupply,
            EnergyConsumptionCost energyConsumptionCost, ResourcesPricesEstimation resourcesPricesEstimation,
            TradeController tradeController)
        {
            this.job = job;
            this.wallet = wallet;
            this.inventory = inventory;
            this.workplace = workplace;
            this.workExperience = workExperience;
            this.demandAndSupply = demandAndSupply;
            this.energyConsumptionCost = energyConsumptionCost;
            this.resourcesPricesEstimation = resourcesPricesEstimation;
            this.tradeController = tradeController;
        }

        public void ChangeJob(WorkTask newJob)
        {
            if (job.WorkTask != null)
                CloseProduction();

            job.WorkTask = newJob;

            UpdateSupplyInfo();
            foreach (var ingredience in job.WorkTask.GetRequiredIngredience())
            {
                demandAndSupply.SetDemand(ingredience.resource, ingredience.count * (AgentsPlanningPerspective.PlanningPerspective / job.WorkTask.time));
            }
        }

        private void UpdateSupplyInfo()
        {
            float supply = WorkOutcomeCalculator.CalculateWorkOutcome(workExperience, job.WorkTask).count * (AgentsPlanningPerspective.PlanningPerspective / job.WorkTask.time);
            demandAndSupply.SetSupply(job.Product.resource, supply);
        }

        public void GoWork()
        {
            RestockSupplies();

            if (!IsWorkInProgress())
            {
                StartNewWork();
            }

            if (IsWorkInProgress())
            {
                if (HasPartialIngerdiences())
                    OnWorkProgress();
                else
                    OnWorkSkip();

                if (IsJobDone())
                    OnWorkFinished();
            }
        }

        public bool IsJobDone()
        {
            return Task.IsDone;
        }

        public bool IsWorkInProgress()
        {
            return Task != null && !Task.IsDone;
        }

        private void StartNewWork()
        {
            Task = new AgentTask(job.WorkTask);
        }

        private void OnWorkProgress()
        {
            Task.UpdateWorked();
            workExperience.IncreaseWorkExperience(Task.WorkTask);

            inventory.AddResource(WorkOutcomeCalculator.CalculatePartialWorkOutcome(workExperience, job.WorkTask));
            foreach (ResourceCountPair ingredient in job.WorkTask.GetRequiredIngredience())
            {
                inventory.SubtractResource(ingredient.resource, ingredient.count / job.WorkTask.time);
            }
        }

        private void OnWorkSkip()
        {
            Task.UpdateSkiped();
        }

        private void OnWorkFinished()
        {           
            UpdateSupplyInfo();
        }

        private bool HasPartialIngerdiences()
        {
            foreach (ResourceCountPair ingredient in job.WorkTask.GetRequiredIngredience())
            {
                if (!inventory.HasResource(new ResourceCountPair(ingredient.resource, ingredient.count / job.WorkTask.time)))
                    return false;
            }

            return true;
        }

        private void RestockSupplies()
        {
            int resourceUnitPrice = resourcesPricesEstimation.GetEstimatedPrice(job.Product.resource);
            float profit = WorkOutcomeCalculator.CalculateWorkOutcome(workExperience, job.WorkTask).count * resourceUnitPrice;

            foreach (ResourceCountPair ingredience in job.WorkTask.GetRequiredIngredience())
            {
                int resToBought = (int)Math.Max(0, ingredience.count - inventory.GetResourceCount(ingredience.resource));
                int acceptablePrice = (int)Math.Max(1, profit / ingredience.count);

                OrderIngredienceResources(ingredience.resource, resToBought, acceptablePrice);
            }
        }

        public void OrderIngredienceResources(Resource resource, int count, int acceptablePrice)
        {
            List<TradeOffer> offers = tradeController.AskForTradeOffers(resource);

            bool succes = true;
            while (count > 0 && succes)
            {
                TradeOffer bestOffer = null;
                float bestRate = float.MaxValue;

                succes = false;
                foreach (TradeOffer offer in offers)
                {
                    if (offer.Price <= acceptablePrice && wallet.GetCurrency() >= offer.Price)
                    {
                        float enegry = EnergyConsumptionCalculator.ConsumeTravelEnergy(workplace.Location, offer.Location);
                        float energyCost = energyConsumptionCost.GetCost(enegry);

                        float rate = offer.Price + energyCost;
                        if (rate < bestRate)
                        {
                            bestRate = rate;
                            bestOffer = offer;
                        }
                    }
                }

                if (bestOffer != null)
                {
                    tradeController.Trade(bestOffer);
                    count--;

                    bestOffer.Count--;
                    if (bestOffer.Count == 0)
                        offers.Remove(bestOffer);

                    succes = true;
                }
            }
        }

        public void CloseProduction()
        {
            demandAndSupply.SetSupply(job.Product.resource, 0);

            foreach (var ingredience in job.WorkTask.GetRequiredIngredience())
            {
                demandAndSupply.SetDemand(ingredience.resource, 0);
            }
        }
    }
}