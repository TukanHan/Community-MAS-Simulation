using Agents;
using Logs;
using System.Linq;
using UI.AgentInfoPanel.List.Rows;
using UI.Base;
using UnityEngine;

namespace UI.AgentInfoPanel.List
{
    public class LogList : UIAgentPropertyListBase
    {
        public override void UpdateList(Agent agent)
        {
            content.ClearChildren();

            var logs = LogManager.instance.GetLogs(new LogsFilter() { Agent = agent, RowsOnPageCount = 30 });
            content.sizeDelta = new Vector2(content.sizeDelta.x, rowHeight * (logs.Count() + 1));

            CreateHeader();

            int index = 1;
            foreach (LogMessage message in logs)
            {
                GameObject item = Instantiate(rowPrefab, content);
                item.GetComponent<LogRow>().Enable(message.ToString());
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index * rowHeight);
                index++;
            }
        }
    }
}