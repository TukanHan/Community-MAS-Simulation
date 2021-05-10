using UnityEngine;
using UnityEngine.UI;

public class ButtonPlaySimulation : MonoBehaviour
{
	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		UIAgentController.instance.Play();
	}
}
