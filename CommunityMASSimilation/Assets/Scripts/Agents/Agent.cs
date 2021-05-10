using Agents.Work;
using Agents.Trade;
using UnityEngine;
using Professions;
using System;
using Logs;
using Agents.WorkplaceSelection;
using Agents.Needs;
using Agents.Core;
using Coordinates;
using Agents.ProfessionSelection;

namespace Agents
{
    public class Agent : MonoBehaviour
    {
        public event EventHandler AgentDead;

        public string AgentName { get; private set; }

        //Base data models
        public Wallet Wallet { get; } = new Wallet(AgentsPlanningPerspective.InitialCurrency);
        public Energy Energy { get; } = new Energy();
        public Inventory Inventory { get; } = new Inventory();
        public AgentWorkTask CurrentJob { get; } = new AgentWorkTask();
        public AgentWorkplace Workplace { get; } = new AgentWorkplace();

        //Preferences
        public WorkExperience WorkExperience { get; private set; } = new WorkExperience();
        public DemandAndSupply DemandAndSupply { get; private set; }
        public ResourcesPricesEstimation ResourcesPricesEstimation { get; private set; }
        public EnergyConsumptionCost EnergyConsumptionCost { get; private set; } = new EnergyConsumptionCost();

        //Controllers
        public TradeController TradeController { get; private set; }
        public WorkController WorkLogic { get; private set; }
        public NeedsController NeedsController { get; set; }
        public ProfessionSelector ProfessionSelector { get; private set; }
        public WorkplaceSelector WorkplaceSelector { get; private set; }

        public void Initialize(System.Random random, string agentName)
        {
            this.AgentName = agentName;

            ResourcesPricesEstimation = new ResourcesPricesEstimation(Inventory);
            DemandAndSupply = new DemandAndSupply(Inventory);

            TradeController = new TradeController(this, Wallet, Inventory, CurrentJob, ResourcesPricesEstimation);
            WorkLogic = new WorkController(Wallet, Inventory, WorkExperience, CurrentJob, Workplace, DemandAndSupply, EnergyConsumptionCost, ResourcesPricesEstimation, TradeController);
            NeedsController = new NeedsController(Wallet, Energy, Inventory, Workplace, DemandAndSupply, EnergyConsumptionCost, ResourcesPricesEstimation, TradeController);
            WorkplaceSelector = new WorkplaceSelector(this, CurrentJob, Workplace, DemandAndSupply);
            ProfessionSelector = new ProfessionSelector(random, Wallet, Energy, Inventory, CurrentJob, WorkExperience, ResourcesPricesEstimation, TradeController);

            ChangeWork(ProfessionSelector.SelectFirstJob());
            WorkplaceSelector.SelectWorkplace(CurrentJob.WorkplaceHexType);
        }

        public void UpdateBehaviour()
        {  
            WorkExperience.DecreaseWorksExperience();
            EnergyConsumptionCost.Update();

            if (!WorkLogic.IsWorkInProgress())
            {
                WorkTask newJob = ProfessionSelector.CheckOutBestJob();
                if(newJob != null)
                {
                    ChangeWork(newJob);
                }

                WorkplaceSelector.SelectWorkplace(CurrentJob.WorkplaceHexType);
            }
                
            WorkLogic.GoWork();
            NeedsController.UpdateConsumption();
            TradeController.EvaluateExport();
            TradeController.EvaluateSale();
        }

        private void ChangeWork(WorkTask newJob)
        {
            LogManager.instance.AddWorkChangeLog(this, this.CurrentJob.WorkTask, newJob);
            WorkLogic.ChangeJob(newJob);
            GetComponent<SpriteRenderer>().sprite = newJob.profession.workplaceImage;
        }

        private void SetDead()
        {
            LogManager.instance.AddDeathLog(this);
            WorkplaceSelector.UnregisterWorkpace();
            WorkLogic.CloseProduction();

            gameObject.SetActive(false);

            if (AgentDead != null)
                AgentDead(this, EventArgs.Empty);
        }

        #region Public Acess Mehtod

        public bool IsAlive()
        {
            return Energy.IsAlive;
        }

        public void Buy(Resource resource, int price, CubeCoordinate sellerLocation)
        {
            Wallet.SubstractCurrency(price);
            Inventory.AddResource(resource, 1);

            Energy.ConsumeEnergy(EnergyConsumptionCalculator.ConsumeTravelEnergy(Workplace.Location, sellerLocation));
        }

        public void Sell(Resource resource, int price)
        {
            Wallet.AddCurrency(price);
            Inventory.SubtractResource(resource, 1);
            ResourcesPricesEstimation.ResourceSold(resource);
        }

        public void Export(Resource resource, int count)
        {
            Wallet.AddCurrency(1 * count);
            Inventory.SubtractResource(resource, count);
        }

        public SupplyDistanceMap AnswerDistanceMap()
        {
            return WorkplaceSelector.AnwserDistanceMap();
        }

        #endregion
    }
}