using Market;
using System;

namespace UI.ResourceTable.Raport
{
    public class UIResourceRaportTableLastPriceRow : UIResourceTableRow
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
                int? lastPrice = Marketplace.instance.GetLastResoureceTradePrice(cell.Key);
                cell.Value.text = $"{(lastPrice.HasValue ? lastPrice.Value.ToString() : "-")}";
            }
        }
    }
}