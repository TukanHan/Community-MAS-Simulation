using Professions;

namespace Logs
{
    public class LogChangeWorkMessage : LogMessage
    {
        public WorkTask NewWork { get; set; }
        public WorkTask PreviousWork { get; set; }

        public override string ToString()
        {
            return $"Runda {Round}: {Agent.AgentName} zmienił pracę z {PreviousWork?.productionName ?? "Brak"} na {NewWork.productionName}";
        }
    }
}