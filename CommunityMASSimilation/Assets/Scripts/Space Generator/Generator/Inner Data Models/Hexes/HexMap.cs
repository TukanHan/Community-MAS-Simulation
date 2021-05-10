using Coordinates;
using System.Collections.Generic;

namespace SpaceGeneration.DataModels
{
    public abstract class HexMap
    {
        public Dictionary<CubeCoordinate, Hex> Map { get; protected set; }
        public List<CubeCoordinate> BorderHexes { get; protected set; }

        public Hex GetHex(CubeCoordinate position)
        {
            return Map[position];
        }

        public virtual bool IsOnMap(CubeCoordinate position)
        {
            return Map.ContainsKey(position);
        }

        public abstract float GetMidPercentDistance(CubeCoordinate position);
    }
}