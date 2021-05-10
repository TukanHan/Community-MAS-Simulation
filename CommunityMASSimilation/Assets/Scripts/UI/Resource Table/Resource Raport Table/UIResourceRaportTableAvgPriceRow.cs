using Market;
using System;

namespace UI.ResourceTable.Raport
{
    public class UIResourceRaportTableAvgPriceRow : UIResourceTableRow
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
                float? price = Marketplace.instance.AskAvgSellPrice(cell.Key, null);
                cell.Value.text = $"{(price.HasValue ? String.Format("{0:0.##}", price) : "-")}";
            }
        }
    }
}