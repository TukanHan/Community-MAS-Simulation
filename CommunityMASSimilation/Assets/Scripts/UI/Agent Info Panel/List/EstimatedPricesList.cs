using Agents;
using UI.AgentInfoPanel.List.Rows;
using UI.Base;
using UnityEngine;

namespace UI.AgentInfoPanel.List
{
    public class EstimatedPricesList : UIAgentPropertyListBase
    {
        public override void UpdateList(Agent agent)
        {
            content.ClearChildren();

            content.sizeDelta = new Vector2(content.sizeDelta.x, rowHeight * (agent.ResourcesPricesEstimation.GetResourcesEstimationsCount() + 1));

            CreateHeader();

            int index = 1;
            foreach (Resource resource in agent.ResourcesPricesEstimation.GetResourcesEstimations())
            {
                GameObject item = Instantiate(rowPrefab, content);
                item.GetComponent<EstimatedPriceRow>().Enable(resource, agent.ResourcesPricesEstimation.GetEstimatedPrice(resource));
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index * rowHeight);
                index++;
            }
        }

    }
}
