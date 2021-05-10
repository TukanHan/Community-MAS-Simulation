using Agents;
using UI.AgentInfoPanel.List;
using UI.Base;
using UnityEngine;

namespace UI.AgentInfoPanel
{
    public class UIAgentInfoTabView : MonoBehaviour
    {
        public UIAgentInfoPanelBackpackList backpackList;
        public EstimatedPricesList estimatedPricesList;
        public SellPriceList sellPriceList;
        public WorkExperienceList workExperienceList;
        public ConsumptionQualityList consumptionQualityList;
        public LogList logList;

        private Agent agent;
        private UIAgentPropertyListBase list;
        private AgentInfoPanelListType listType;

        public void Enable(Agent agent)
        {
            this.agent = agent;
            list = SwitchListType(listType);
        }

        public void Update()
        {
            list.UpdateList(agent);
        }

        public void SelectListType(AgentInfoPanelListType listType)
        {
            this.listType = listType;
            list = SwitchListType(listType);
            list.ResetPosition();
            list.UpdateList(agent);
        }

        private UIAgentPropertyListBase SwitchListType(AgentInfoPanelListType listType)
        {
            switch (listType)
            {
                case AgentInfoPanelListType.Inventory:
                    return backpackList;
                case AgentInfoPanelListType.EstimatedPrices:
                    return estimatedPricesList;
                case AgentInfoPanelListType.SellPrices:
                    return sellPriceList;
                case AgentInfoPanelListType.WorkExperience:
                    return workExperienceList;
                case AgentInfoPanelListType.ConsumptionQuality:
                    return consumptionQualityList;
                case AgentInfoPanelListType.Logs:
                    return logList;
                default:
                    throw new System.InvalidOperationException();
            }
        }
    }
}
