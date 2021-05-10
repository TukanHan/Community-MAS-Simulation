using Coordinates;
using SpaceGeneration.DataModels;
using System;
using System.Collections.Generic;

namespace SpaceGeneration.Generation
{
    public class WaterMapGenerator
    {
        private readonly Random random;
        private readonly HexSO waterHex;
        private readonly NoiseMapGenerator noiseMapGenerator;
        private readonly NoiseMapNormalizer noiseMapNormalizer;
        private readonly NoiseMapTheresholdSelector noiseMapTheresholdSelector;


        public WaterMapGenerator(Random random, HexSO waterHex)
        {
            this.random = random;
            this.waterHex = waterHex;
            this.noiseMapGenerator = new NoiseMapGenerator(random);
            this.noiseMapNormalizer = new NoiseMapNormalizer();
            this.noiseMapTheresholdSelector = new NoiseMapTheresholdSelector();
        }

        public void GenerateWaterMap(HexMap hexMap, PercentNoiseMapParametersModel waterNoiseMapParameters)
        {
            var noiseMap = noiseMapGenerator.Generate(waterNoiseMapParameters, hexMap);
            noiseMapNormalizer.NormalizeNoiseMap(noiseMap);
            float waterThereshold = noiseMapTheresholdSelector.SelectThereshold(noiseMap, random.RandomFloat(waterNoiseMapParameters.PercentRange));

            foreach (var hex in hexMap.Map)
            {
                if (noiseMap[hex.Key] >= waterThereshold)
                {
                    hex.Value.HexModel = waterHex;
                }
                    

                hex.Value.WaterDeepness = noiseMap[hex.Key];
            }
        }

        private Dictionary<CubeCoordinate, float> GenerateOceanNoiseMap(HexMap hexMap, PercentNoiseMapParametersModel oceanNoiseMapParameters)
        {
            Dictionary<CubeCoordinate, float> oceanNoiseMap = noiseMapGenerator.Generate(oceanNoiseMapParameters, hexMap);
            foreach (var hex in hexMap.Map)
            {
                float midDistance = hexMap.GetMidPercentDistance(hex.Key);
                if (midDistance < 0.9f)
                    oceanNoiseMap[hex.Key] *= (float)Math.Pow(0.1f + midDistance, 2);
                else
                    oceanNoiseMap[hex.Key] = 1;
            }

            return noiseMapNormalizer.NormalizeNoiseMap(oceanNoiseMap, 0.3f);
        }

        private void MarkWaterHex(CubeCoordinate pos, HexMap hexMap, List<CubeCoordinate> markedHexes)
        {
            markedHexes.Add(pos);
            hexMap.GetHex(pos).HexModel = waterHex;

            for (int i = 0; i < 6; ++i)
            {
                CubeCoordinate newPos = pos + CubeCoordinate.Neighbours[i];
                if (hexMap.IsOnMap(newPos) && hexMap.GetHex(newPos).HexModel.hexType == HexType.Water && !markedHexes.Contains(newPos))
                    MarkWaterHex(newPos, hexMap, markedHexes);
            }
        }
    }
}