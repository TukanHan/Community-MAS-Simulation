using Agents;
using System;

namespace UI.ResourceTable.Price
{
    public class UIResourceDemandTableRow : UIResourceTableAgentRow
    {
        protected override void UpdateData()
        {
            foreach (var cell in dataCells)
            {
                float demand = agent.DemandAndSupply.AnswerDemand(cell.Key);
                cell.Value.text = $"{(demand == 0 ? "-" : String.Format("{0:0.##}", demand))}";
            }
        }
    }
}

