using UnityEngine;
using UnityEngine.UI;

public class ButtonIncreaseStepCount : MonoBehaviour
{
	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		UIAgentController.instance.ChangeSepsCount(1);
	}
}
