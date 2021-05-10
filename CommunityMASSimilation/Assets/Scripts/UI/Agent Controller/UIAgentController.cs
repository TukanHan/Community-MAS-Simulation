using UnityEngine;
using UnityEngine.UI;

public class UIAgentController : SingletonBehaviour<UIAgentController>
{
    public Text roundValueText;
    public Text stepsText;

    private int stepCount = 1;

    public void Start()
    {
        AgentQueueController.instance.NextRoundStarted += UpdateRoundValueText;
    }

    public void Pause()
    {
        AgentQueueController.instance.Pause();
    }

    public void Play()
    {
        AgentQueueController.instance.Play();
    }

    public void NextStep()
    {
        AgentQueueController.instance.NextRound();
    }

    public void MultipleNextStep()
    {
        AgentQueueController.instance.NextRound(stepCount);
    }

    public void ChangeSepsCount(sbyte change)
    {
        stepCount = Mathf.Clamp(stepCount + change, 1, 1000);
        stepsText.text = stepCount.ToString();
    }

    private void UpdateRoundValueText(object sender, int round)
    {
        roundValueText.text = round.ToString();
    }
}