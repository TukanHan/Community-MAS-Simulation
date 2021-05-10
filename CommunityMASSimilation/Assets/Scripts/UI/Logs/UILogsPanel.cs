using Agents;
using Logs;
using System.Collections.Generic;
using System.Linq;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Logs
{
    public class UILogsPanel : SingletonBehaviour<UILogsPanel>, IUIPanel
    {
        public RectTransform contentPanel;
        public GameObject rowPrefab;

        //Filter
        public Dropdown logTypeDropdown;
        public Dropdown agentDropdown;
        public InputField roundFromInput;
        public InputField roundToInput;
        public Slider rowsOnPageCountSlider;

        private ListCanvas<LogMessage> listCanvas;
        private Dictionary<string, global::Logs.LogType?> logTypeDictionary;
        private Dictionary<string, Agent> agentsDictionary;

        private void PrepareFilter()
        {
            logTypeDictionary = new Dictionary<string, global::Logs.LogType?>();
            logTypeDictionary["Wszystkie"] = null;
            //logTypeDictionary["Śmierć"] = global::Logs.LogType.Death;
            logTypeDictionary["Zmiana miejsca"] = global::Logs.LogType.WorkplaceChange;
            logTypeDictionary["Zmiana pracy"] = global::Logs.LogType.WorkChange;
            logTypeDictionary["Zakup"] = global::Logs.LogType.Purchase;
            logTypeDictionary["Sprzedarz"] = global::Logs.LogType.Sale;

            logTypeDropdown.AddOptions(logTypeDictionary.Keys.ToList());

            agentsDictionary = new Dictionary<string, Agent>();
            agentsDictionary["Wszyscy"] = null;
            foreach (Agent agent in AgentQueueController.instance.Agents)
            {
                agentsDictionary[agent.AgentName] = agent;
            }
            agentDropdown.AddOptions(agentsDictionary.Keys.ToList());
        }

        public void Enable(object[] data)
        {
            if(logTypeDictionary == null)
                PrepareFilter();

            //contentPanel.ClearChildren();
            gameObject.SetActive(true);   
        }

        public void Search()
        {
            listCanvas = new ListCanvas<LogMessage>(contentPanel);

            BuildRows();
        }

        private void BuildRows()
        {
            foreach (var log in LogManager.instance.GetLogs(GetFilterData()))
            {
                GameObject go = Instantiate(rowPrefab, contentPanel);
                UILogRow row = go.GetComponent<UILogRow>();
                row.Enable(log.ToString());
                listCanvas.AddRow(log, row);
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            
            if(listCanvas != null)
                listCanvas.Remove();
        }

        private LogsFilter GetFilterData()
        {
            uint? roundFrom = null;
            if (!string.IsNullOrEmpty(roundFromInput.text)) 
                roundFrom = uint.Parse(roundFromInput.text);

            uint? roundTo = null;
            if (!string.IsNullOrEmpty(roundToInput.text))
                roundTo = uint.Parse(roundToInput.text);

            return new LogsFilter()
            {
                RowsOnPageCount = (int)rowsOnPageCountSlider.value,
                RoundFrom = roundFrom,
                RoundTo = roundTo,
                Agent = agentsDictionary[agentDropdown.options[agentDropdown.value].text],
                LogType = logTypeDictionary[logTypeDropdown.options[logTypeDropdown.value].text]
            };
        }
    }
}