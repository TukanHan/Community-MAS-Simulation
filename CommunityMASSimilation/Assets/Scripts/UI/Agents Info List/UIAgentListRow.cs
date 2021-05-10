using Agents;
using Agents.Work;
using System;
using UI.AgentInfoPanel;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentsList
{
    public class UIAgentListRow : MonoBehaviour, IUIPanel, IListElement
    {
        public Text agentNameText;
        public Text professionText;
        public Text progressText;
        public ProgressBar progressBar;
        public MaslovBar maslovBar;
        public Text energyValueText;
        public Text moneyCountText;
        public UIAgentPanelButton showAgentInfoPanelButton;

        private Agent agent;
        private RectTransform rectTransform;

        public void Enable(params object[] obj)
        {
            agent = obj[0] as Agent;
            rectTransform = GetComponent<RectTransform>();
            UpdateData();

            AgentQueueController.instance.NextRoundStarted += UpdateDataCallback;
            showAgentInfoPanelButton.Initialize(agent);
        }

        private void UpdateDataCallback(object sender, int round)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            agentNameText.text = agent.AgentName;
            professionText.text = $"{agent.CurrentJob.WorkTask.productionName}";
            energyValueText.text = $"{String.Format("{0:0.}%", Mathf.Ceil(agent.Energy.CalculateSatisfaction() * 100))}";
            moneyCountText.text = agent.Wallet.GetCurrency().ToString();

            AgentTask task = agent.WorkLogic.Task;
            if (task != null)
            {
                progressBar.gameObject.SetActive(true);
                progressBar.SetPercent((task.WorkTask.time - task.RemainingTime) / (float)task.WorkTask.time);
                progressText.gameObject.SetActive(false);
            }
            else
            {
                progressBar.gameObject.SetActive(false);
                progressText.gameObject.SetActive(true);
            }

            maslovBar.SetPercent(agent.Energy);
        }

        public void Disable()
        {
            AgentQueueController.instance.NextRoundStarted -= UpdateDataCallback;
            Destroy(gameObject);
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }
    }
}