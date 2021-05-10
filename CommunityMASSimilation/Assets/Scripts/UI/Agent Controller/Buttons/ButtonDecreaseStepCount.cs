using UnityEngine;
using UnityEngine.UI;

public class ButtonDecreaseStepCount : MonoBehaviour
{
	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		UIAgentController.instance.ChangeSepsCount(-1);
	}
}
