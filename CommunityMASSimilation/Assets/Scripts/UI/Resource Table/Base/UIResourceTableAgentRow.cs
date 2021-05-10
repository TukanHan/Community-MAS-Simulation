using Agents;
using System.Collections.Generic;
using UI.AgentInfoPanel;
using UnityEngine;

namespace UI.ResourceTable
{
    public abstract class UIResourceTableAgentRow : UIResourceTableRow
    {
        public UIAgentPanelButton agentPanelButton;
        protected Agent agent;

        public override void Enable(params object[] data)
        {
            agent = data[1] as Agent;
            labelText.text = agent.AgentName;

            base.Enable(data);
        }

        protected override float BuildRow(List<Resource> resOrder)
        {
            float min = base.BuildRow(resOrder);

            GameObject go = Instantiate(agentPanelButton.gameObject, transform);

            go.GetComponent<RectTransform>().anchorMin = new Vector2(min, 0.1f);
            go.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.9f);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

            go.GetComponent<UIAgentPanelButton>().Initialize(agent);

            return 1;
        }
    }
}