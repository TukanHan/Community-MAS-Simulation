using Agents;
using Agents.Work;
using System;
using UI.AgentInfoPanel.List;
using UI.Base;
using UI.WorkplacePreference;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel
{
    public class UIAgentInfoPanel : SingletonBehaviour<UIAgentInfoPanel>, IUIPanel
    {
        public RectTransform panel;
        public Text agentNameText;
        public Text agentWorkTaskText;
        public MaslovBar maslovBar;
        public Text locationText;
        public Button locationButton;
        public Text happineseValText;
        public Text foodNeedsValText;
        public Text primaryNeedsValText;
        public Text luxuryNeedsValText;
        public Text currencyValText;
        public Text workTaskProgressText;
        public ProgressBar workTaskProgressbar;
        public UIAgentInfoTabView list;

        private Agent agent;

        private void Start()
        {
            locationButton.onClick.AddListener(OpenAgentPreferenceMap);
        }

        public void Enable(object[] obj)
        {
            agent = obj[0] as Agent;
            agent.AgentDead += AgentDeadCallback;

            panel.gameObject.SetActive(true);
            list.Enable(agent);
            UpdateData();

            AgentQueueController.instance.NextRoundStarted += UpdateDataCallback;
        }

        private void UpdateDataCallback(object sender, int round)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            agentNameText.text = agent.AgentName.ToString();
            agentWorkTaskText.text = agent.CurrentJob.WorkTask?.productionName;
            maslovBar.SetPercent(agent.Energy);

            Coordinates.CubeCoordinate locationCoordinates = agent.Workplace.Location;
            locationText.text = $"({locationCoordinates.x},{locationCoordinates.y},{locationCoordinates.z})";
            happineseValText.text = $"{String.Format("{0:0.}%", Mathf.Ceil(agent.Energy.CalculateSatisfaction() * 100))}";
            foodNeedsValText.text = $"{String.Format("{0:0.}%", Mathf.Ceil(agent.Energy.FoodNeeds * 100))}";
            primaryNeedsValText.text = $"{String.Format("{0:0.}%", Mathf.Ceil(agent.Energy.BasicNeeds * 100))}";
            luxuryNeedsValText.text = $"{String.Format("{0:0.}%", Mathf.Ceil(agent.Energy.LuxuryNeeds * 100))}";
            currencyValText.text = agent.Wallet.GetCurrency().ToString();

            AgentTask task = agent.WorkLogic.Task;
            if (task == null)
            {
                workTaskProgressText.gameObject.SetActive(true);
                workTaskProgressbar.gameObject.SetActive(false);
            }
            else
            {
                workTaskProgressText.gameObject.SetActive(false);
                workTaskProgressbar.gameObject.SetActive(true);
                workTaskProgressbar.SetPercent((task.WorkTask.time - task.RemainingTime) / (float)task.WorkTask.time);
            }

            list.Update();

            if(agent.IsAlive())
                MapTextInfoManager.instance.EnableMapInfo(WorkplacePreferenceMapService.PrepareLocationData(agent));
        }

        private void AgentDeadCallback(object sender, EventArgs eventArgs)
        {
            agent.AgentDead -= AgentDeadCallback;
            MapTextInfoManager.instance.DisableMapInfo();
        }

        public void Disable()
        {
            panel.gameObject.SetActive(false);
            agent.AgentDead -= AgentDeadCallback;
            AgentQueueController.instance.NextRoundStarted -= UpdateDataCallback;
            MapTextInfoManager.instance.DisableMapInfo();
        }

        public void ChangeListType(AgentInfoPanelListType listyType)
        {
            list.SelectListType(listyType);
        }

        private void OpenAgentPreferenceMap()
        {
            UIPanelsController.instance.EnableWorkplaceSelectionPreferenceMap(agent);
        }
    }
}