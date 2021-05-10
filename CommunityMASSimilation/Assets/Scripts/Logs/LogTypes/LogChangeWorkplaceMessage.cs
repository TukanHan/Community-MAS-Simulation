using Coordinates;

namespace Logs
{
    public class LogChangeWorkplaceMessage : LogMessage
    {
        public CubeCoordinate NewWorkplaceLocation { get; set; }
        public CubeCoordinate? PreviousWorkplaceLocation { get; set; }

        public override string ToString()
        {
            return $"Runda {Round}: {Agent.AgentName} zmienił miejsce pracy z {PreviousWorkplaceLocation?.ToString() ?? "Brak"} na {NewWorkplaceLocation.ToString()}";
        }
    }
}
