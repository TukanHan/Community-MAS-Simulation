using System;
using Coordinates;
using System.Linq;
using System.Collections.Generic;
using SpaceGeneration.DataModels;

namespace SpaceGeneration.Generation
{
    public class RangeResourceDistributionGenerator : IResourceDistributionGenerator
    {
        private readonly Random random;
        private readonly RangeResourceDistributionModel resourceDistribution;
        private readonly HexMap hexMap;

        private Dictionary<CubeCoordinate, bool> blockedFields;

        public RangeResourceDistributionGenerator(Random random, HexMap hexMap, RangeResourceDistributionModel resourceDistribution)
        {
            this.hexMap = hexMap;
            this.random = random;
            this.resourceDistribution = resourceDistribution;
        }

        public void GenerateResources()
        {
            blockedFields = new Dictionary<CubeCoordinate, bool>();

            foreach (var key in hexMap.Map.Keys)
                blockedFields[key] = false;

            foreach (var hex in hexMap.Map)
            {
                if (hex.Value.HexModel.hexType == HexType.Plain)
                    GenerateResourceOnTile(hex.Key);
            }
        }

        private void GenerateResourceOnTile(CubeCoordinate pos)
        {
            Hex hex = hexMap.GetHex(pos);
            if (hex.HexModel.hexType == HexType.Plain && blockedFields[pos] == false)
            {
                if (random.NextDouble() < resourceDistribution.Threshold)
                {
                    hex.HexModel = resourceDistribution.Resource;
                    DrawResourceOnMap(pos, resourceDistribution.Range);
                }
            }
        }

        private void DrawResourceOnMap(CubeCoordinate position, int range)
        {
            var coordinates = CubeCooridinatesManager
                .GetHexInRange(position, range)
                .Where(x => hexMap.IsOnMap(x))
                .ToList();

            foreach (var coordinate in coordinates)
                blockedFields[coordinate] = true;
        }
    }
}
