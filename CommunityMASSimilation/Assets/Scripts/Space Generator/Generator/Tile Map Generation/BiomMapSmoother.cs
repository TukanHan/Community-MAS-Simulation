using System.Linq;
using System.Collections.Generic;
using Coordinates;
using SpaceGeneration.DataModels;

namespace SpaceGeneration.Generation
{
    public class BiomMapSmoother
    {
        private HexMap map;

        private Stack<CubeCoordinate> positionToCheck = new Stack<CubeCoordinate>();

        public void SmoothBiomMap(HexMap hexMap)
        {
            this.map = hexMap;

            foreach (var hex in hexMap.Map)
            {
                positionToCheck.Push(hex.Key);
            }

            int loopCount = hexMap.Map.Count * 2;
            while (positionToCheck.Count > 0)
            {
                SmoothBiom(positionToCheck.Pop());

                if (--loopCount < 0)
                    break;
            }
        }

        private void SmoothBiom(CubeCoordinate pos)
        {
            int countOfSameBiom = 0;
            List<CubeCoordinate> neighbours = new List<CubeCoordinate>();

            for (int i = 0; i < 6; ++i)
                AddIfIsNeighbour(pos, CubeCoordinate.Neighbours[i], neighbours, ref countOfSameBiom);

            if (countOfSameBiom < 2)
            {
                HexSO mostFrequentBiom = neighbours.GroupBy(x => map.GetHex(x).HexModel)
                    .Select(x => new { biom = x, cnt = x.Count() })
                    .OrderByDescending(g => g.cnt)
                    .Select(g => g.biom).First().Key;

                map.GetHex(pos).HexModel = mostFrequentBiom;
                neighbours.ForEach(x => positionToCheck.Push(x));
            }
        }

        private void AddIfIsNeighbour(CubeCoordinate pos, CubeCoordinate offset, List<CubeCoordinate> neighbours, ref int countOfSameBiom)
        {
            CubeCoordinate changedPos = pos + offset;
            if (map.IsOnMap(changedPos))
            {
                if (map.GetHex(pos).HexModel == map.GetHex(changedPos).HexModel)
                    countOfSameBiom++;

                neighbours.Add(changedPos);
            }
        }
    }
}