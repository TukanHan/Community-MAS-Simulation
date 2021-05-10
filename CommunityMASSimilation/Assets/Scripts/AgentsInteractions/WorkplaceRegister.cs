using Agents;
using Agents.WorkplaceSelection;
using Coordinates;
using DataModels;
using SpaceGeneration.DataModels;
using SpaceGeneration.UnityPort;
using System.Collections.Generic;
using System.Linq;

public class WorkplaceRegister : SingletonBehaviour<WorkplaceRegister>
{
    private HexDataModelDictionary Map { get { return SpaceGenerator.instance.Map; } }

    public bool IsThereWorkplaceFor(HexType hexType)
    {
        return Map
            .Select(hex => hex.Value)
            .Where(hex => hex.hexTypeInfo.hexType == hexType && hex.Worker == null)
            .Any();
    }

    public List<CubeCoordinate> GetPossibleWorkplaces(HexType hexType)
    {
        return Map
            .Where(hex => hex.Value.hexTypeInfo.hexType == hexType && hex.Value.Worker == null)
            .Select(hex => hex.Key)
            .ToList();
    }

    public void RegisterInWorkplace(Agent agent, HexDataModel workplace)
    {
        workplace.RegisterWorker(agent);
    }

    public void UnregisterFromWorkplace(HexDataModel workplace)
    {
        if (workplace != null)
            workplace.UnregisterWorker();
    }

    public List<SupplyDistanceMap> AskForDiscancesMap(Resource resource, Agent querist)
    {
        List<SupplyDistanceMap> distanceMaps = new List<SupplyDistanceMap>();
        foreach (Agent agent in AgentQueueController.instance.GetAliveAgents())
        {
            if(agent != querist && agent.CurrentJob.Product.resource == resource)
            {
                distanceMaps.Add(agent.AnswerDistanceMap());
            }
        }

        return distanceMaps;
    }
}