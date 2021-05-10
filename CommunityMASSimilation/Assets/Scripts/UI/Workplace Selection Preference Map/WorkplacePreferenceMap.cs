using Agents.WorkplaceSelection;
using Coordinates;
using System;
using System.Collections.Generic;

namespace UI.WorkplacePreference
{
    public class WorkplacePreferenceMap
    {
        public CubeCoordinate? AgentLocation { get; set; }
        public DistanceMap DistanceMap { get; set; }
        public List<Tuple<CubeCoordinate, Resource>> Supplayers { get; set; }
    }
}
