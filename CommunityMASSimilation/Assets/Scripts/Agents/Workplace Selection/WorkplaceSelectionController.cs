using Agents.Core;
using Agents.Work;
using Coordinates;
using DataModels;
using Logs;
using SpaceGeneration.DataModels;
using SpaceGeneration.UnityPort;
using System.Collections.Generic;
using System.Linq;

namespace Agents.WorkplaceSelection
{
    public class WorkplaceSelector
    {
        private DistanceMap distanceMap;

        readonly Agent owner;
        readonly AgentWorkTask currentJob;
        readonly AgentWorkplace workplace;
        readonly DemandAndSupply demandAndSupply;

        public WorkplaceSelector(Agent owner, AgentWorkTask currentJob, AgentWorkplace workplace, DemandAndSupply demandAndSupply)
        {
            this.demandAndSupply = demandAndSupply;
            this.currentJob = currentJob;
            this.workplace = workplace;
            this.owner = owner;

            distanceMap = new DistanceMap(SpaceGenerator.instance.Map.Keys);
        }

        public void SelectWorkplace(HexType workplaceHexType)
        {
            CubeCoordinate? oldLocation = workplace.Workplace?.coordinate;

            UnregisterWorkpace();
            HexDataModel newWorkplace = SelectBestWorkplacePosition(workplaceHexType);
            ChangeWorkplace(newWorkplace);
            SetDistanceMap(newWorkplace.coordinate);

            if(!oldLocation.HasValue || !oldLocation.Value.Equals(newWorkplace.coordinate))
                LogManager.instance.AddWorkplaceChangeLog(owner, oldLocation, newWorkplace.coordinate);
        }

        private HexDataModel SelectBestWorkplacePosition(HexType hexType)
        {
            List<CubeCoordinate> possibleWorkplaces = WorkplaceRegister.instance.GetPossibleWorkplaces(hexType);

            DistanceMap supplyDistanceMap = BuildSupplyDistanceMap(possibleWorkplaces);

            KeyValuePair<CubeCoordinate, float> max = new KeyValuePair<CubeCoordinate, float>(); 
            foreach (var pair in supplyDistanceMap)
            {
                if (pair.Value >= max.Value)
                    max = pair;
            }

            return SpaceGenerator.instance.Map[max.Key];
        }

        public DistanceMap BuildSupplyDistanceMap(List<CubeCoordinate> possibleWorkplaces)
        {
            DistanceMap supplyDistanceMap = new DistanceMap(possibleWorkplaces);

            foreach (var demandedResource in demandAndSupply.AnswerDemands())
            {
                DistanceMap distanceMap = BuildResourceSupplyDistanceMap(demandedResource, possibleWorkplaces);
                float demand = demandAndSupply.AnswerDemand(demandedResource);

                foreach (var pair in distanceMap)
                {
                    supplyDistanceMap[pair.Key] += distanceMap[pair.Key] * demand;
                }
            }

            return supplyDistanceMap;
        }

        private DistanceMap BuildResourceSupplyDistanceMap(Resource resource, List<CubeCoordinate> possibleWorkplaces)
        {
            List<SupplyDistanceMap> maps = WorkplaceRegister.instance.AskForDiscancesMap(resource, owner);
            float supplySum = maps.Sum(map => map.Supply);
            DistanceMap resourceSupplyDistanceMap = new DistanceMap(possibleWorkplaces);

            if (maps.Count == 0)
                return resourceSupplyDistanceMap;

            foreach(SupplyDistanceMap map in maps)
            {
                float multiplier = map.Supply / supplySum;
                foreach(CubeCoordinate coordinate in possibleWorkplaces)
                {
                    resourceSupplyDistanceMap[coordinate] += map.DistanceMap[coordinate] * multiplier;
                }
            }

            float maxValue = resourceSupplyDistanceMap.Max(x => x.Value);

            foreach (var coordinate in possibleWorkplaces)
            {
                resourceSupplyDistanceMap[coordinate] = (1 - resourceSupplyDistanceMap[coordinate]/maxValue);
            }

            return resourceSupplyDistanceMap;
        }

        private void ChangeWorkplace(HexDataModel newWorkplace)
        {
            owner.transform.position = SpaceGenerator.WorldPosition(new Coordinate(newWorkplace.coordinate));
            WorkplaceRegister.instance.RegisterInWorkplace(owner, newWorkplace);
            workplace.Workplace = newWorkplace;
        }

        public void UnregisterWorkpace()
        {
            WorkplaceRegister.instance.UnregisterFromWorkplace(workplace.Workplace);
        }

        #region Distance Map
        private void SetDistanceMap(CubeCoordinate location)
        {
            foreach (CubeCoordinate coordinate in SpaceGenerator.instance.Map.Keys)
            {
                distanceMap[coordinate] = CubeCooridinatesManager.CalculateDistance(coordinate, location);
            }
        }

        public SupplyDistanceMap AnwserDistanceMap()
        {
            return new SupplyDistanceMap()
            {
                DistanceMap = distanceMap,
                Supply = demandAndSupply.AnswerSupply(currentJob.Product.resource)
            };
        }
        #endregion
    }
}
