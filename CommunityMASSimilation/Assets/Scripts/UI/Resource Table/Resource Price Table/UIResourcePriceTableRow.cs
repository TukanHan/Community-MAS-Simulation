using Agents;
using System;

namespace UI.ResourceTable.Price
{
    public class UIResourcePriceTableRow : UIResourceTableAgentRow
    {        
        protected override void UpdateData()
        {
            var prices = agent.TradeController.SaleAnnouncements;
            foreach (var cell in dataCells)
            {
                cell.Value.text = prices.ContainsKey(cell.Key) ? prices[cell.Key].Price.ToString() : "-";
            }
        }
    }
}
