using Agents;
using UI.AgentInfoPanel.List.Rows;
using UI.Base;
using UnityEngine;

namespace UI.AgentInfoPanel.List
{
    public class WorkExperienceList : UIAgentPropertyListBase
    {
        public override void UpdateList(Agent agent)
        {
            content.ClearChildren();

            content.sizeDelta = new Vector2(content.sizeDelta.x, rowHeight * (agent.WorkExperience.WorksExperiences.Count + 1));

            CreateHeader();

            int index = 1;
            foreach (var pair in agent.WorkExperience.WorksExperiences)
            {
                GameObject item = Instantiate(rowPrefab, content);
                item.GetComponent<WorkExperienceRow>().Enable(pair);
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index * rowHeight);
                index++;
            }
        }
    }
}