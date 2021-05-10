using Agents;
using UI.AgentInfoPanel.List.Rows;
using UI.Base;
using UnityEngine;

namespace UI.AgentInfoPanel.List
{
    public class SellPriceList : UIAgentPropertyListBase
    {
        public override void UpdateList(Agent agent)
        {
            content.ClearChildren();

            content.sizeDelta = new Vector2(content.sizeDelta.x, rowHeight * (agent.TradeController.SaleAnnouncements.Count + 1));

            CreateHeader();

            int index = 1;
            foreach (var pair in agent.TradeController.SaleAnnouncements)
            {
                GameObject item = Instantiate(rowPrefab, content);
                item.GetComponent<TradePriceRow>().Enable(pair.Key, pair.Value.Price, pair.Value.Count);
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index * rowHeight);
                index++;
            }
        }
    }
}