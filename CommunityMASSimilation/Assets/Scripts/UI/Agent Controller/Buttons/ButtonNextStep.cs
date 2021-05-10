using UnityEngine;
using UnityEngine.UI;

public class ButtonNextStep : MonoBehaviour
{
	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		UIAgentController.instance.NextStep();
	}
}
