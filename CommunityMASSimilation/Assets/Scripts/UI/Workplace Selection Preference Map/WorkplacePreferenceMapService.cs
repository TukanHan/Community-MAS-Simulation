using Agents;
using Coordinates;
using SpaceGeneration.UnityPort;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.WorkplacePreference
{
    public static class WorkplacePreferenceMapService
    {
        public static WorkplacePreferenceMap PrepareData(Agent agent)
        {
            List<CubeCoordinate> fields = GetPossibleWorkplaces(agent);
            return new WorkplacePreferenceMap()
            {
                AgentLocation = agent.Workplace.Location,
                DistanceMap = agent.WorkplaceSelector.BuildSupplyDistanceMap(fields),
                Supplayers = GetSupplayers(agent)
            };
        }

        public static WorkplacePreferenceMap PrepareSuplayersData()
        {
            return new WorkplacePreferenceMap()
            {
                Supplayers = GetAllSupplayers()
            };
        }

        public static WorkplacePreferenceMap PrepareLocationData(Agent agent)
        {
            return new WorkplacePreferenceMap()
            {
                AgentLocation = agent.Workplace.Location
            };
        }

        private static List<CubeCoordinate> GetPossibleWorkplaces(Agent agent)
        {
            return SpaceGenerator.instance.Map
                .Where(hex => hex.Value.hexTypeInfo.hexType == agent.Workplace.FieldType &&
                       (hex.Value.Worker == null || hex.Value.Worker == agent))
                .Select(hex => hex.Key)
                .ToList();
        }

        private static List<Tuple<CubeCoordinate, Resource>> GetSupplayers(Agent agent)
        {
            return AgentQueueController.instance.GetAliveAgents()
                .Where(a => a != agent && agent.DemandAndSupply.AnswerDemands().Contains(a.CurrentJob.Product.resource))
                .Select(a => new Tuple<CubeCoordinate, Resource>(a.Workplace.Location, a.CurrentJob.Product.resource))
                .ToList();
        }

        private static List<Tuple<CubeCoordinate, Resource>> GetAllSupplayers()
        {
            return AgentQueueController.instance.GetAliveAgents()
                .Select(a => new Tuple<CubeCoordinate, Resource>(a.Workplace.Location, a.CurrentJob.Product.resource))
                .ToList();
        }
    }
}
