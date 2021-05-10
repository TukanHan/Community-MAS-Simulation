using Market;
using System;

namespace UI.ResourceTable.Raport
{
    public class UIResourceRaportTableDemandSupplyBalanceRow : UIResourceTableRow
    {
        public override void Enable(params object[] data)
        {
            labelText.text = data[1] as string;
            base.Enable(data);
        }

        protected override void UpdateData()
        {
            foreach (var cell in dataCells)
            {
                cell.Value.text = GetText(Marketplace.instance.GetSupply(null, cell.Key), Marketplace.instance.GetDemand(null, cell.Key));
            }
        }

        private string GetText(float supply, float demand)
        {
            if (supply == 0 || demand == 0)
                return "-";
            else
                return $"{String.Format("{0:0.##}", supply / demand)}";
        }
    }
}