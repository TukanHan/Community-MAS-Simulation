using Coordinates;
using System;
using System.Collections.Generic;

//namespace MapGenerator.Generator
//{
//    public class MountainGenerator
//    {
//        private readonly Random random;
//        private readonly HexMap hexMap;
//        private readonly NoiseMapTheresholdSelector noiseMapTheresholdSelector;

//        public MountainGenerator(Random random, HexMap hexMap)
//        {
//            this.random = random;
//            this.hexMap = hexMap;
//            this.noiseMapTheresholdSelector = new NoiseMapTheresholdSelector();
//        }

//        public void GenerateMountain(PercentNoiseMapParametersModel mountainNoiseMapParameters)
//        {
//            var noiseMap = new NoiseMapGenerator(random).Generate(mountainNoiseMapParameters, hexMap);
//            CalculateValue(hexMap, noiseMap);
//            new NoiseMapNormalizer().NormalizeNoiseMap(noiseMap);
            
//            float[] treresholds = CalculateTheresholds(mountainNoiseMapParameters.PercentRange, noiseMap);
//            CalculateBiome(hexMap, noiseMap, treresholds);
//        }

//        private float[] CalculateTheresholds(MinMax range, Dictionary<CubeCoordinate, float> noiseMap)
//        {
//            float[] randomValues = new float[3];
//            randomValues[0] = random.RandomFloat(range);
//            randomValues[1] = random.RandomFloat(randomValues[0] * 0.33f, randomValues[0] * 0.66f);
//            randomValues[2] = random.RandomFloat(randomValues[1] * 0.33f, randomValues[1] * 0.66f);

//            return noiseMapTheresholdSelector.SelectThereshold(noiseMap, randomValues, CanGenerateMountain);
//        }

//        private void CalculateValue(HexMap hexMap, Dictionary<CubeCoordinate, float> noiseMap)
//        {
//            foreach(var hex in hexMap.Map)
//            {
//                noiseMap[hex.Key] = (noiseMap[hex.Key] - hex.Value.WaterDeepness) * hex.Value.Biom.MountainTolerance;
//            }
//        }

//        private void CalculateBiome(HexMap hexMap, Dictionary<CubeCoordinate, float> noiseMap, float[] treresholds)
//        {
//            foreach(var hex in hexMap.Map)
//            {
//                hex.Value.MountainHeight = CalculateMountainHeight(noiseMap[hex.Key], treresholds);

//                if (noiseMap[hex.Key] >= treresholds[0] && CanGenerateMountain(hex.Key))
//                    hex.Value.Biom = hex.Value.Biom.MountainBiom;
//            }
//        }

//        private uint CalculateMountainHeight(float value, float[] treresholds)
//        {
//            if (value > treresholds[2])
//                return 3;
//            else if (value > treresholds[1])
//                return 2;
//            else if (value >= treresholds[0])
//                return 1;
//            else
//                return 0;
//        }

//        public bool CanGenerateMountain(CubeCoordinate position)
//        {
//            return !hexMap.GetHex(position).Biom.IsWaterBiom;
//        }
//    }
//}
