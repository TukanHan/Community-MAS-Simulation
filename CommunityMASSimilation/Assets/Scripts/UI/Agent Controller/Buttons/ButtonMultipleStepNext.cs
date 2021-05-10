using UnityEngine;
using UnityEngine.UI;

public class ButtonMultipleStepNext : MonoBehaviour
{
	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		UIAgentController.instance.MultipleNextStep();
	}
}
