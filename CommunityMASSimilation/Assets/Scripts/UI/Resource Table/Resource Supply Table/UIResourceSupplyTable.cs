using Agents;
using Market;
using System;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.ResourceTable.Supply
{
    public class UIResourceSupplyTable : SingletonBehaviour<UIResourceSupplyTable>, IUIPanel
    {
        public UIResourceSupplyTableRow supplyRowPrefab;
        public UIResourcesTableHeader header;

        public RectTransform panel;
        public RectTransform contentPanel;
        private List<Resource> resOrder;
        private ListCanvas<Agent> listCanvas;

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

            listCanvas = new ListCanvas<Agent>(contentPanel);
            panel.gameObject.SetActive(true);

            BuildRows();
        }

        private void BuildRows()
        {
            foreach (var agent in AgentQueueController.instance.GetAliveAgents())
            {
                GameObject go = Instantiate(supplyRowPrefab.gameObject, contentPanel);
                UIResourceTableRow row = go.GetComponent<UIResourceTableRow>();
                row.Enable(resOrder, agent);
                listCanvas.AddRow(agent, row);

                agent.AgentDead += DeleteRowCallback;
            }
        }

        private void DeleteRowCallback(object sender, EventArgs eventArgs)
        {
            Agent agent = sender as Agent;
            listCanvas.RemoveRow(agent);
            agent.AgentDead -= DeleteRowCallback;
        }

        public void Disable()
        {
            foreach (var agent in AgentQueueController.instance.GetAliveAgents())
            {
                agent.AgentDead -= DeleteRowCallback;
            }

            panel.gameObject.SetActive(false);
            listCanvas.Remove();
        }
    }
}