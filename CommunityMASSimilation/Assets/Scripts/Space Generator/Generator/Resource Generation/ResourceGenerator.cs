using SpaceGeneration.DataModels;
using System;

namespace SpaceGeneration.Generation
{
    public class ResourceGenerator
    {
        private readonly Random random;
        private readonly ResourceDistributionGeneratorFactory factory;

        public ResourceGenerator(Random random)
        {
            this.random = random;
            this.factory = new ResourceDistributionGeneratorFactory(random);
        }

        public void GenerateResource(HexMap tilesMap, ResourceDistributionModel[] resourceDistibutions)
        {
            foreach(var resourceDistribution in resourceDistibutions)
            {
                IResourceDistributionGenerator resourceDistributionGenerator = factory
                    .GetResourceDistributionGenerator(tilesMap, resourceDistribution);

                resourceDistributionGenerator.GenerateResources();
            }
        }
    }
}
