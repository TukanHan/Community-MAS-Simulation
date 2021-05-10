using Agents;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel
{
    public class UIAgentPanelButton : MonoBehaviour
    {
        public void Initialize(Agent agent)
        {
            GetComponent<Button>().onClick.AddListener(() => ShowAgentInfoPanel(agent));
        }

        private void ShowAgentInfoPanel(Agent agent)
        {
            UIPanelsController.instance.EnableAgentInfoPanel(agent);
        }
    }
}