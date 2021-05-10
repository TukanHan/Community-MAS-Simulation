using System;
using System.Collections.Generic;

namespace Coordinates
{
    public static class CubeCooridinatesManager
    {
        public static IEnumerable<CubeCoordinate> GetHexInRange(CubeCoordinate center, int range)
        {
            for(int x = -range; x <= range; ++x)
            {
                for (int y = Math.Max(-range, -x-range), maxY = Math.Min(range, -x + range); y <= maxY; ++y)
                {
                    yield return (center + new CubeCoordinate(x, y, -x - y));
                }
            }
        }

        public static IEnumerable<CubeCoordinate> GetHexInCircle(CubeCoordinate center, int radius)
        {
            CubeCoordinate pos = (center + CubeCoordinate.Neighbours[4]) * radius;

            for (int i = 0; i < 6; ++i)
            {
                for (int j = 0; j < radius; ++j)
                {
                    yield return pos;
                    pos += CubeCoordinate.Neighbours[i];
                }
            }
        }

        #region Distance
        private static int CalculateCoordinateLenght(CubeCoordinate hex)
        {
            return (Math.Abs(hex.x) + Math.Abs(hex.y) + Math.Abs(hex.z)) >> 1;
        }

        public static int CalculateDistance(CubeCoordinate A, CubeCoordinate B)
        {
            return CalculateCoordinateLenght(A - B);
        }
        #endregion
    }
}