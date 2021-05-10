using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel.List
{
	public class UIAgentInfoPanelListTypeSelectorButton : MonoBehaviour
	{
		public AgentInfoPanelListType infoPanelListType;

		void Start()
		{
			Button btn = GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
		}

		public void TaskOnClick()
		{
			UIAgentInfoPanel.instance.ChangeListType(infoPanelListType);
		}
	}
}