using Agents;
using UI.AgentInfoPanel.List.Rows;
using UI.Base;
using UnityEngine;

namespace UI.AgentInfoPanel.List
{
    public class UIAgentInfoPanelBackpackList : UIAgentPropertyListBase
    {
        public override void UpdateList(Agent agent)
        {
            content.ClearChildren();

            content.sizeDelta = new Vector2(content.sizeDelta.x, rowHeight * (agent.Inventory.GetItemsCount() + 1));

            CreateHeader();

            int index = 1;
            foreach (var key in agent.Inventory.GetItemsList())
            {
                GameObject item = Instantiate(rowPrefab, content);
                item.GetComponent<BackpackRow>().Enable(new ResourceCountPair(key, agent.Inventory.GetResourceCount(key)));
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index * rowHeight);
                index++;
            }
        }
    }
}