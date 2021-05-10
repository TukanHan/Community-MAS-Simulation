using UI.Base;
using UI.WorkplacePreference;

namespace UI.RsourceSupplayersMap
{
    public class UIResourceSupplayersMap : SingletonBehaviour<UIResourceSupplayersMap>, IUIPanel
    {
        public void Enable(params object[] data)
        {
            AgentQueueController.instance.NextRoundStarted += UpdateDataCallback;
            UpdateData();
        }

        private void UpdateDataCallback(object sender, int round)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            WorkplacePreferenceMap data = WorkplacePreferenceMapService.PrepareSuplayersData();
            MapTextInfoManager.instance.EnableMapInfo(data);
        }

        public void Disable()
        {
            AgentQueueController.instance.NextRoundStarted -= UpdateDataCallback;
            MapTextInfoManager.instance.DisableMapInfo();
        }
    }
}