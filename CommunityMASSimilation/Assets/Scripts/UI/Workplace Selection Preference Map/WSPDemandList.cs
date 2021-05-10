using Agents;
using System.Linq;
using UI.Base;
using UnityEngine;

namespace UI.WorkplacePreference
{
    public class WSPDemandList : UIAgentPropertyListBase
    {
        public override void UpdateList(Agent agent)
        {
            content.ClearChildren();

            var demandOrderedList = agent.DemandAndSupply.AnswerDemands()
                .Select(key => new { key, value = agent.DemandAndSupply.AnswerDemand(key) })
                .OrderByDescending(obj => obj.value)
                .ToList();

            content.sizeDelta = new Vector2(content.sizeDelta.x, rowHeight * (demandOrderedList.Count() + 1));

            CreateHeader();

            int index = 1;
            foreach (var demand in demandOrderedList)
            {
                GameObject item = Instantiate(rowPrefab, content);
                item.GetComponent<WSPDemandRow>().Enable(demand.key, demand.value);
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index * rowHeight);
                index++;
            }
        }
    }
}
