using Agents.Trade;
using Agents.Work;
using Professions;
using System;

namespace Agents.ProfessionSelection
{
    public class ProfessionSelector
    {
        private class RatedWorkTask
        {
            public float Rate { get; set; }
            public WorkTask Job { get; set; }
        }

        readonly Random random;
        readonly Wallet wallet;
        readonly Energy energy;
        readonly Inventory inventory;
        readonly AgentWorkTask currentJob;
        readonly WorkExperience workExperience;
        readonly ResourcesPricesEstimation resourcesPricesEstimation;
        readonly TradeController tradeController;

        public ProfessionSelector(Random random, Wallet wallet, Energy energy, Inventory inventory, AgentWorkTask currentJob,
            WorkExperience workExperience, ResourcesPricesEstimation resourcesPricesEstimation, TradeController tradeController)
        {
            this.random = random;
            this.currentJob = currentJob;
            this.wallet = wallet;
            this.energy = energy;
            this.inventory = inventory;
            this.workExperience = workExperience;
            this.resourcesPricesEstimation = resourcesPricesEstimation;
            this.tradeController = tradeController;
        }

        public WorkTask SelectFirstJob()
        {
             return SelectBestJob().Job;  
        }

        public WorkTask CheckOutBestJob()
        {
            RatedWorkTask bestWork = SelectBestJob();
            if (bestWork.Rate > RateJob(currentJob.WorkTask) * (1 + energy.CalculateSatisfaction()))
                return bestWork.Job;

            return null;
        }

        private RatedWorkTask SelectBestJob()
        {
            WorkTask bestWorkTask = null;
            float bestWorkTaskRate = float.MinValue;

            foreach (WorkTask possibleJob in EmploymentAgency.instance.professions)
            {
                if(WorkplaceRegister.instance.IsThereWorkplaceFor(possibleJob.profession.workplaceHexType))
                {
                    float rate = RateJob(possibleJob) * random.RandomFloat(1, 1.1f);
                    if (bestWorkTask == null || bestWorkTaskRate < rate)
                    {
                        bestWorkTask = possibleJob;
                        bestWorkTaskRate = rate;
                    }
                }
            }

            return new RatedWorkTask() { Job = bestWorkTask, Rate = bestWorkTaskRate };
        }

        private float RateJob(WorkTask job)
        {
            float taskExecutionsCount = AgentsPlanningPerspective.PlanningPerspective / job.time;

            float cost = 0;
            foreach (ResourceCountPair pair in job.GetRequiredIngredience())
            {
                //Posiadane sztuki
                float ownedResourceCount = inventory.GetResourceCount(pair.resource);

                //Średnia cena sztuki
                int avgIngrediencePrice = resourcesPricesEstimation.GetEstimatedPrice(pair.resource);

                //Sprawdzić ile jest na rynku
                int howManyToPurchase = tradeController.AskResourcePurchaseCount(pair.resource);

                //Na ile mnie jeszcze stać
                int howManyCanPurchase = Math.Min(wallet.GetCurrency() / avgIngrediencePrice, howManyToPurchase);

                
                //Sprawdzić czy mam na conajmniej 1 rundę
                if (howManyCanPurchase + ownedResourceCount >= pair.count)
                {
                    //Max posiadanych towarów, które mogły być użyte
                    int maxUsableOwnedResourceCount = (int)Math.Min(ownedResourceCount, pair.count * taskExecutionsCount);

                    //Liczę ile trzeba jeszcze dokupić do końca procesu
                    int resourcesCountToBought = (int)(taskExecutionsCount * pair.count) - maxUsableOwnedResourceCount;

                    //Tylę dożucę do interesu
                    cost = (resourcesCountToBought * avgIngrediencePrice) + (maxUsableOwnedResourceCount * avgIngrediencePrice * 0.5f);
                }
                else
                {
                    //Nie stać mnie
                    return 0;
                }
            }

            ResourceCountPair outcomeProduct = WorkOutcomeCalculator.CalculateWorkOutcome(workExperience, job);
            int resourceProductPrice = resourcesPricesEstimation.GetEstimatedPrice(outcomeProduct.resource);

            float supply = tradeController.AskResourceSupply(outcomeProduct.resource) + (outcomeProduct.count * taskExecutionsCount);
            float demand = tradeController.AskResourceDemand(outcomeProduct.resource) + (outcomeProduct.count * taskExecutionsCount);

            float balance = supply / demand;

            float profit = (resourceProductPrice/ balance) * outcomeProduct.count * taskExecutionsCount;

            return profit - cost;
        }
    }
}