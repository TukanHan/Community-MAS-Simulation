using Agents;
using System;
using UI.ResourceTable.Price;

namespace UI.ResourceTable.Supply
{
    public class UIResourceSupplyTableRow : UIResourceTableAgentRow
    {
        protected override void UpdateData()
        {
            foreach (var cell in dataCells)
            {
                float supply = agent.DemandAndSupply.AnswerSupply(cell.Key);
                cell.Value.text = $"{(supply == 0 ? "-" : String.Format("{0:0.##}", supply))}";
            }
        }
    }
}