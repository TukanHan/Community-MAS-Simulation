using Market;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.ResourceTable.Raport
{
    public class UIResourceRaportTable : SingletonBehaviour<UIResourceRaportTable>, IUIPanel
    {
        public UIResourcesTableHeader header;
        public GameObject demandRow;
        public GameObject supplyRow;
        public GameObject demandSupplyBalanceRow;
        public GameObject avgPriceRow;
        public GameObject lastPriceRow;
        public GameObject resCountRow;

        public RectTransform panel;
        public RectTransform contentPanel;
        private List<Resource> resOrder;
        private ListCanvas<GameObject> listCanvas;

        public void Instantine()
        {
            resOrder = new List<Resource>();
            resOrder.AddRange(Marketplace.instance.IngredienceResources);
            resOrder.AddRange(Marketplace.instance.FoodResources);
            resOrder.AddRange(Marketplace.instance.BasicResources);
            resOrder.AddRange(Marketplace.instance.LuxuryResources);

            header.Set(resOrder);
        }

        public void Enable(object[] data)
        {
            if (resOrder == null)
                Instantine();

            listCanvas = new ListCanvas<GameObject>(contentPanel);
            panel.gameObject.SetActive(true);

            BuildRow(demandRow, "Popyt");
            BuildRow(supplyRow, "Podaż");
            BuildRow(demandSupplyBalanceRow, "Podaż / Popyt");
            BuildRow(avgPriceRow, "Średnia cena");
            BuildRow(lastPriceRow, "Ostatnia transtakcja");
            BuildRow(resCountRow, "Ilość towarów");
        }

        private void BuildRow(GameObject rowPrefab, string label)
        {
            GameObject go = Instantiate(rowPrefab, contentPanel);
            UIResourceTableRow row = go.GetComponent<UIResourceTableRow>();
            row.Enable(resOrder, label);
            listCanvas.AddRow(rowPrefab, row);
        }

        public void Disable()
        {
            panel.gameObject.SetActive(false);
            listCanvas.Remove();
        }
    }
}