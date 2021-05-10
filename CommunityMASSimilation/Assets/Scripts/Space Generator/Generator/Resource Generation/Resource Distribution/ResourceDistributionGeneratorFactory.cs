using System;
using SpaceGeneration.DataModels;

namespace SpaceGeneration.Generation
{
    public class ResourceDistributionGeneratorFactory
    {
        private readonly Random random;

        public ResourceDistributionGeneratorFactory(Random random)
        {
            this.random = random;
        }

        public IResourceDistributionGenerator GetResourceDistributionGenerator(HexMap tilesMap, ResourceDistributionModel resourceDistribution)
        {
            if (resourceDistribution is NoiseMapResourceDistributionModel)
                return new NoiseMapResourceDistibutionGenerator(random, tilesMap, resourceDistribution as NoiseMapResourceDistributionModel);
            //else if (resourceDistribution is RandomResourceDistributionModel)
            //    return new RandomResourceDistributionGenerator(random, tilesMap, resourceDistribution as RandomResourceDistributionModel);
            else if (resourceDistribution is RangeResourceDistributionModel)
                return new RangeResourceDistributionGenerator(random, tilesMap, resourceDistribution as RangeResourceDistributionModel);
            else
                throw new InvalidOperationException();
        }
    }
}
