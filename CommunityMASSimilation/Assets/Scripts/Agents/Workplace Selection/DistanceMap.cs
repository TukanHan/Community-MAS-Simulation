using Coordinates;
using System.Collections.Generic;

namespace Agents.WorkplaceSelection
{
    public class DistanceMap : Dictionary<CubeCoordinate, float>
    {
        public DistanceMap(IEnumerable<CubeCoordinate> hexList)
        {
            foreach(var coordinate in hexList)
            {
                Add(coordinate, 0);
            }
        }
    }
}
