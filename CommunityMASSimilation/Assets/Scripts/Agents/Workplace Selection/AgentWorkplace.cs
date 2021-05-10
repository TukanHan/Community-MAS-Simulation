using Coordinates;
using DataModels;
using SpaceGeneration.DataModels;

namespace Agents.WorkplaceSelection
{
    public class AgentWorkplace
    {
        public HexDataModel Workplace { get; set; }
        public CubeCoordinate Location { get { return Workplace.coordinate; } }
        public HexType FieldType { get { return Workplace.hexTypeInfo.hexType; } }
    }
}
