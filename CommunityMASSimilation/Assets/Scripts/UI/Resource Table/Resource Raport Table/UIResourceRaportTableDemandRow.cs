using Market;
using System;

namespace UI.ResourceTable.Raport
{
    public class UIResourceRaportTableDemandRow : UIResourceTableRow
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
                cell.Value.text = $"{String.Format("{0:0.##}", Marketplace.instance.GetDemand(null, cell.Key))}";
            }
        }
    }
}