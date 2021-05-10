using Agents;
using SpaceGeneration.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WorkplacePreference
{
    public class UIWorkplacePreference : SingletonBehaviour<UIWorkplacePreference>, IUIPanel
    {
        public RectTransform panel;
        public Dropdown agentDropdown;
        public WSPDemandList demandList;
        public TextLabelValuePair hextTypeText;

        const string SelectAgentText = "Wybierz agenta";

        private Dictionary<Agent, Dropdown.OptionData> agentOptions = new Dictionary<Agent, Dropdown.OptionData>();
        private Agent selectedAgent = null;

        private void Start()
        {
            agentDropdown.onValueChanged.AddListener(SelectedAgentCallback);
        }

        public void Enable(params object[] data)
        {
            panel.gameObject.SetActive(true);
            ClearDropdown();

            if (data != null)
                SelectedAgentCallback(data[0] as Agent);

            AgentQueueController.instance.NextRoundStarted += UpdateDataCallback;
        }

        private void ClearDropdown()
        {
            selectedAgent = null;

            agentOptions.Clear();
            agentDropdown.ClearOptions();
            agentDropdown.options.Add(new Dropdown.OptionData(SelectAgentText));
            agentDropdown.captionText.text = SelectAgentText;

            foreach (Agent agent in AgentQueueController.instance.GetAliveAgents())
            {
                agentDropdown.options.Add(agentOptions[agent] = new Dropdown.OptionData(agent.AgentName));
                agent.AgentDead += DeleteOptionCallback;
            }

            demandList.gameObject.SetActive(false);
            hextTypeText.Disable();
        }

        private void UpdateDataCallback(object sender, int round)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            if(selectedAgent == null)
            {
                MapTextInfoManager.instance.DisableMapInfo();
                demandList.gameObject.SetActive(false);
                hextTypeText.Disable();
            }
            else
            {
                hextTypeText.SetValue(selectedAgent.Workplace.FieldType.GetHexTypeName());
                WorkplacePreferenceMap data = WorkplacePreferenceMapService.PrepareData(selectedAgent);
                MapTextInfoManager.instance.EnableMapInfo(data);
                demandList.gameObject.SetActive(true);
                demandList.UpdateList(selectedAgent);
            }
        }

        private void DeleteOptionCallback(object sender, EventArgs eventArgs)
        {
            Agent agent = sender as Agent;

            agentDropdown.options.Remove(agentOptions[agent]);
            agentOptions.Remove(agent);
            agent.AgentDead -= DeleteOptionCallback;

            int value = 0;
            if (agent != selectedAgent && selectedAgent != null)
            {
                for(; value < agentDropdown.options.Count; value++)
                {
                    if (agentDropdown.options[value].text == agentOptions[selectedAgent].text)
                        break;
                }
            }

            agentDropdown.value = value;
        }

        private void SelectedAgentCallback(int value)
        {
            string agentName = agentDropdown.options[agentDropdown.value].text;
            SelectedAgentCallback(agentOptions.Where(x => x.Value.text == agentName).Select(y => y.Key).FirstOrDefault());
        }

        private void SelectedAgentCallback(Agent selected)
        {
            if (selected != selectedAgent)
            {
                selectedAgent = selected;
                UpdateData();
            }
        }

        public void Disable()
        {
            panel.gameObject.SetActive(false);

            AgentQueueController.instance.NextRoundStarted -= UpdateDataCallback;
            MapTextInfoManager.instance.DisableMapInfo();

            foreach (var agent in agentOptions.Keys)
            {
                agent.AgentDead -= DeleteOptionCallback;
            }
        }
    }
}