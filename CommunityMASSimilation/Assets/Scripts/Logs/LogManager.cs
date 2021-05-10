using Agents;
using Coordinates;
using Professions;
using System.Collections.Generic;
using System.Linq;

namespace Logs
{
    public class LogManager : SingletonBehaviour<LogManager>
    {
        private List<LogMessage> logs = new List<LogMessage>();
        private uint LogCount = 0;

        public void AddPurchageLog(Agent seller, Agent buyer, Resource resource, int price)
        {
            var log = new LogPurchaseMessage()
            {
                LogNr = LogCount++,
                Seller = seller,
                Agent = buyer,
                LogType = LogType.Purchase,
                Resource = resource,
                Price = price,
                Round = AgentQueueController.instance.Round
            };

            logs.Add(log);
        }

        public void AddSaleLog(Agent seller, Agent buyer, Resource resource, int price)
        {
            var log = new LogSaleMessage()
            {
                LogNr = LogCount++,
                Buyer = buyer,
                Agent = seller,
                LogType = LogType.Sale,
                Resource = resource,
                Price = price,
                Round = AgentQueueController.instance.Round
            };

            logs.Add(log);
        }

        public void AddDeathLog(Agent agent)
        {
            var log = new LogDeathMessage()
            {
                LogNr = LogCount++,
                Agent = agent,
                LogType = LogType.Death,
                Round = AgentQueueController.instance.Round
            };

            logs.Add(log);
        }

        public void AddWorkChangeLog(Agent agent, WorkTask previousWork, WorkTask newWork)
        {
            var log = new LogChangeWorkMessage()
            {
                LogNr = LogCount++,
                Agent = agent,
                PreviousWork = previousWork,
                NewWork = newWork,
                LogType = LogType.WorkChange,
                Round = AgentQueueController.instance.Round
            };

            logs.Add(log);
        }

        public void AddWorkplaceChangeLog(Agent agent, CubeCoordinate? previousLocation, CubeCoordinate newLocation)
        {
            var log = new LogChangeWorkplaceMessage()
            {
                LogNr = LogCount++,
                Agent = agent,
                PreviousWorkplaceLocation = previousLocation,
                NewWorkplaceLocation = newLocation,
                LogType = LogType.WorkplaceChange,
                Round = AgentQueueController.instance.Round
            };

            logs.Add(log);
        }

        public IEnumerable<LogMessage> GetLogs(LogsFilter filter)
        {
            IEnumerable<LogMessage> selectedLogs = logs;

            if (filter.LogType != null)
                selectedLogs = selectedLogs.Where(l => l.LogType == filter.LogType);

            if (filter.Agent != null)
                selectedLogs = selectedLogs.Where(l => l.Agent == filter.Agent);

            if (filter.RoundFrom != null)
                selectedLogs = selectedLogs.Where(l => l.Round >= filter.RoundFrom);

            if (filter.RoundTo != null)
                selectedLogs = selectedLogs.Where(l => l.Round <= filter.RoundTo);

            return selectedLogs
                .OrderByDescending(l => l.LogNr)
                .Take(filter.RowsOnPageCount);
        }
    }
}