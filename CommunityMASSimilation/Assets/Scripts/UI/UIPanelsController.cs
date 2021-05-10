using Agents;
using UI.AgentInfoPanel;
using UI.AgentsList;
using UI.Base;
using UI.Logs;
using UI.ResourceTable.Demand;
using UI.ResourceTable.Price;
using UI.ResourceTable.Raport;
using UI.ResourceTable.Supply;
using UI.RsourceSupplayersMap;
using UI.WorkplacePreference;

namespace UI
{
    public class UIPanelsController : SingletonBehaviour<UIPanelsController>
    {
        IUIPanel currentPanel = null;

        public void EnableAgentList()
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UIAgentList.instance;
            currentPanel.Enable(null);
        }

        public void EnableAgentInfoPanel(Agent agent)
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UIAgentInfoPanel.instance;
            currentPanel.Enable(agent);
        }

        public void EnableResourceSalesPricesTable()
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UIResourcePriceTable.instance;
            currentPanel.Enable(null);
        }

        public void EnableResourceDemandTable()
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UIResourceDemandTable.instance;
            currentPanel.Enable(null);
        }

        public void EnableResourceSupplyTable()
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UIResourceSupplyTable.instance;
            currentPanel.Enable(null);
        }

        public void EnableResourcesRaport()
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UIResourceRaportTable.instance;
            currentPanel.Enable(null);
        }

        public void EnableWorkplaceSelectionPreferenceMap()
        {
            EnableWorkplaceSelectionPreferenceMap(null);
        }

        public void EnableWorkplaceSelectionPreferenceMap(Agent agent)
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UIWorkplacePreference.instance;
            currentPanel.Enable(agent);
        }

        public void EnableLogs()
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UILogsPanel.instance;
            currentPanel.Enable(null);
        }

        public void EnableResourceSupplayersMap()
        {
            if (currentPanel != null)
                currentPanel.Disable();

            currentPanel = UIResourceSupplayersMap.instance;
            currentPanel.Enable(null);
        }

        public void Disable()
        {
            if (currentPanel != null)
                currentPanel.Disable();
        }
    }
}