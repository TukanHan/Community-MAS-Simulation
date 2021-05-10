using SpaceGeneration.DataModels;
using System;

namespace SpaceGeneration.Generation
{
    public class Generator
    {
        public HexMap HexMap { get; set; }

        private readonly HexSO plainHex;
        private readonly HexSO waterHex;

        private readonly PercentNoiseMapParametersModel waterNoiseMapParameters;

        private readonly ResourceDistributionModel[] resources;

        private readonly Random random;

        public Generator(int seed, int range,
                         HexSO plainHex, HexSO waterHex,
                         PercentNoiseMapParametersModel waterNoiseMapParameters,
                         ResourceDistributionModel[] resources)
        {
            this.plainHex = plainHex; 
            this.waterHex = waterHex;
            this.waterNoiseMapParameters = waterNoiseMapParameters;
            this.resources = resources;

            random = new Random(seed);

            HexMap = new HexagonalShapeSpaceGenerator(range, plainHex);
        }

        public void Generate()
        {
            WaterMapGenerator waterMapGenerator = new WaterMapGenerator(random, waterHex);
            waterMapGenerator.GenerateWaterMap(HexMap, waterNoiseMapParameters);

            BiomMapSmoother biomMapSmoother = new BiomMapSmoother();
            biomMapSmoother.SmoothBiomMap(HexMap);

            ResourceGenerator resourceGenerator = new ResourceGenerator(random);
            resourceGenerator.GenerateResource(HexMap, resources);
        }
    }
}