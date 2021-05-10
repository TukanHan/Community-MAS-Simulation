using SpaceGeneration.DataModels;
using System;

namespace SpaceGeneration.Generation
{
    public class BiomMapGenerator
    {
        private readonly Random random;
        private readonly HexSO[,] hexModels;
        private readonly NoiseMapGenerator noiseMapGenerator;
        private readonly NoiseMapNormalizer noiseMapNormalizer;
        private readonly RangeNoiseMapParametersModel temperatureNoiseMapParam;
        private readonly RangeNoiseMapParametersModel heightNoiseMapParam;

        private float temperatureLayerCountInversion;
        private float heightLayerCountInversion;

        public BiomMapGenerator(Random random,
                                HexSO[,] hexModels,
                                RangeNoiseMapParametersModel temperatureNoiseMapParameters,
                                RangeNoiseMapParametersModel heightNoiseMapParameters)
        {
            this.random = random;
            this.noiseMapGenerator = new NoiseMapGenerator(random);
            this.noiseMapNormalizer = new NoiseMapNormalizer();
            this.temperatureNoiseMapParam = temperatureNoiseMapParameters;
            this.heightNoiseMapParam = heightNoiseMapParameters;
            this.hexModels = hexModels;
        }

        public void GenerateBiomMap(HexMap hexMap)
        {
            var heightNoiseArray = noiseMapGenerator.Generate(heightNoiseMapParam, hexMap);
            heightNoiseArray = noiseMapNormalizer.NormalizeNoiseMap(heightNoiseArray, heightNoiseMapParam.Range.Min, heightNoiseMapParam.Range.Max);

            var temperatureNoiseArray = noiseMapGenerator.Generate(temperatureNoiseMapParam, hexMap);
            temperatureNoiseArray = noiseMapNormalizer.NormalizeNoiseMap(temperatureNoiseArray, temperatureNoiseMapParam.Range.Min, temperatureNoiseMapParam.Range.Max);

            temperatureLayerCountInversion = 1f / hexModels.GetLength(1);
            heightLayerCountInversion = 1f / hexModels.GetLength(0);

            foreach (var hex in hexMap.Map)
            {
                hex.Value.Height = ScaleValue(heightNoiseArray[hex.Key], heightNoiseMapParam.Range);
                hex.Value.Temperature = ScaleValue(temperatureNoiseArray[hex.Key], temperatureNoiseMapParam.Range);
                hex.Value.HexModel = CalculateBiom(hex.Value.Temperature, hex.Value.Height);
            }
        }

        private HexSO CalculateBiom(float temperature, float height)
        {
            int x = (int)(temperature / temperatureLayerCountInversion);
            int y = (int)(height / heightLayerCountInversion);

            return hexModels[y, x];
        }

        private float ScaleValue(float value, MinMax range)
        {
            return range.CalculateRange() * value + range.Min;
        }
    }
}