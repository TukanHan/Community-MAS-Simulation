using Agents;
using System.Linq;
using UI.AgentInfoPanel.List.Rows;
using UI.Base;
using UnityEngine;

namespace UI.AgentInfoPanel.List
{
    public class ConsumptionQualityList : UIAgentPropertyListBase
    {
        public override void UpdateList(Agent agent)
        {
            content.ClearChildren();

            var list = agent.NeedsController.NeedsConsumptionBase
                .Select(n => n.Value.ResourceConsumptionEnergyValue)
                .SelectMany(m => m)
                .Select(m => new { resource = m.Key, percent = m.Value})
                .ToList();

            content.sizeDelta = new Vector2(content.sizeDelta.x, rowHeight * (list.Count + 1));

            CreateHeader();

            int index = 1;
            foreach (var element in list)
            {
                GameObject item = Instantiate(rowPrefab, content);
                item.GetComponent<ConsumptionQualityRow>().Enable(element.resource, element.percent);
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index * rowHeight);
                index++;
            }
        }

    }
}
