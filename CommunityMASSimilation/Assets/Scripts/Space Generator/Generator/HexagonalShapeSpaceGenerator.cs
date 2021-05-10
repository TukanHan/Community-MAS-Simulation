using Coordinates;
using SpaceGeneration.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceGeneration
{
    public class HexagonalShapeSpaceGenerator : HexMap
    {
        public int Radius { get; }

        public HexagonalShapeSpaceGenerator(int range, HexSO nullHex)
        {
            Radius = range;

            Map = new Dictionary<CubeCoordinate, Hex>();

            for (int x = -range; x <= range; ++x)
            {
                for (int y = Math.Max(-range, -x - range), maxY = Math.Min(range, -x + range); y <= maxY; ++y)
                {
                    Map[new CubeCoordinate(x, y, -x - y)] = new Hex() { HexModel = nullHex };
                }
            }

            FillBorderHexes();
        }

        public override bool IsOnMap(CubeCoordinate position)
        {
            return Math.Abs(position.x) <= Radius && Math.Abs(position.y) <= Radius && Math.Abs(position.z) <= Radius;
        }

        public void FillBorderHexes()
        {
            BorderHexes = CubeCooridinatesManager.GetHexInCircle(new CubeCoordinate(0, 0, 0), Radius).ToList();
        }

        public override float GetMidPercentDistance(CubeCoordinate pos)
        {
            return new[] { Math.Abs(pos.x), Math.Abs(pos.y), Math.Abs(pos.z) }.Max() / (float)Radius;
        }
    }
}
