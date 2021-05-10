using Agents;
using System;
using UI.Base;
using UnityEngine;

namespace UI.AgentsList
{
    public class UIAgentList : SingletonBehaviour<UIAgentList>, IUIPanel
    {
        public GameObject rowPrefab;
        public RectTransform panel;
        public RectTransform contentPanel;

        private ListCanvas<Agent> listCanvas;

        public void Enable(object[] data)
        {
            contentPanel.ClearChildren();
            panel.gameObject.SetActive(true);

            listCanvas = new ListCanvas<Agent>(contentPanel);

            BuildRows();
        }

        private void BuildRows()
        {
            foreach (var agent in AgentQueueController.instance.GetAliveAgents())
            {
                GameObject go = Instantiate(rowPrefab, contentPanel);
                UIAgentListRow row = go.GetComponent<UIAgentListRow>();
                row.Enable(agent);
                listCanvas.AddRow(agent, row);

                agent.AgentDead += DeleteRowCallback;
            }
        }

        private void DeleteRowCallback(object sender, EventArgs eventArgs)
        {
            Agent agent = sender as Agent;
            listCanvas.RemoveRow(agent);
            agent.AgentDead -= DeleteRowCallback;
        }

        public void Disable()
        {
            foreach (var agent in AgentQueueController.instance.GetAliveAgents())
            {
                agent.AgentDead -= DeleteRowCallback;
            }

            panel.gameObject.SetActive(false);
            listCanvas.Remove();
        }
    }
}