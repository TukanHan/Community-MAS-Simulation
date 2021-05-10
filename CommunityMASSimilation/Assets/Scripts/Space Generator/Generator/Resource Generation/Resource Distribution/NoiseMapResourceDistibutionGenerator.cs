using System;
//using Randomnes;
using Coordinates;
using System.Collections.Generic;
using SpaceGeneration.DataModels;

namespace SpaceGeneration.Generation
{
    public class NoiseMapResourceDistibutionGenerator : IResourceDistributionGenerator
    {
        private readonly Random random;
        private readonly HexMap hexMap;
        private readonly NoiseMapGenerator noiseMapGenerator;
        private readonly NoiseMapNormalizer noiseMapNormalizer;
        //private readonly RouletteWheelSelector rouletteWheelSelector;
        private readonly NoiseMapTheresholdSelector noiseMapTheresholdSelector;
        private readonly NoiseMapResourceDistributionModel resourceDistribution;

        public NoiseMapResourceDistibutionGenerator(Random random, HexMap hexMap, NoiseMapResourceDistributionModel resourceDistribution)
        {
            this.random = random;
            this.hexMap = hexMap;
            //this.rouletteWheelSelector = new RouletteWheelSelector(random);
            this.noiseMapNormalizer = new NoiseMapNormalizer();
            this.noiseMapGenerator = new NoiseMapGenerator(random);
            this.noiseMapTheresholdSelector = new NoiseMapTheresholdSelector();
            this.resourceDistribution = resourceDistribution;
        }

        public void GenerateResources()
        {
            var noiseMap = noiseMapGenerator.Generate(resourceDistribution.NoiseMapParametersModel, hexMap);
            CalculateValue(hexMap, noiseMap, resourceDistribution);
            noiseMapNormalizer.NormalizeNoiseMap(noiseMap);
          
            float thereshold = noiseMapTheresholdSelector.SelectThereshold(noiseMap, random.RandomFloat(resourceDistribution.NoiseMapParametersModel.PercentRange), (pos => CanGenerateOnHex(pos)));

            foreach(var noise in noiseMap)
            {
                Hex hex = hexMap.GetHex(noise.Key);
                GenerateResourceOnTile(hex, noise.Value, resourceDistribution, thereshold);
            }
        }

        public void CalculateValue(HexMap hexMap, Dictionary<CubeCoordinate, float> noiseMap, ResourceDistributionModel resourceDistribution)
        {
            foreach (var hex in hexMap.Map)
            {
                float tolerance = 0.0001f;

                if (CanGenerateOnHex(hex.Key))
                    tolerance = resourceDistribution.Threshold;

                noiseMap[hex.Key] *= tolerance;
            }
        }

        private void GenerateResourceOnTile(Hex hex, float noiseValue, ResourceDistributionModel resourceDistribution, float thereshold)
        {
            if (hex.HexModel.hexType == HexType.Plain)
            {
                if (noiseValue >= thereshold)
                {
                    hex.HexModel = resourceDistribution.Resource;
                }
            }
        }

        private bool CanGenerateOnHex(CubeCoordinate coordinate)
        {
            return hexMap.GetHex(coordinate).HexModel.hexType == HexType.Plain;
        }
    }
}
