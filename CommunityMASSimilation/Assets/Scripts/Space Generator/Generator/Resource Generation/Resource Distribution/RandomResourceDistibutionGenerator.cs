using System;
using SpaceGeneration.DataModels;

namespace SpaceGeneration.Generation
{
    //public class RandomResourceDistributionGenerator : IResourceDistributionGenerator
    //{
    //    private readonly Random random;
    //    private readonly HexMap hexMap;
    //    private readonly RouletteWheelSelector rouletteWheelSelector;
    //    private readonly ResourceDistributionModel resourceDistribution;

    //    public RandomResourceDistributionGenerator(Random random, HexMap hexMap, ResourceDistributionModel resourceDistribution)
    //    {
    //        this.random = random;
    //        this.hexMap = hexMap;
    //        this.rouletteWheelSelector = new RouletteWheelSelector(random);
    //        this.resourceDistribution = resourceDistribution;
    //    }

    //    public void GenerateResources()
    //    {
    //        foreach(var hex in hexMap.Map)
    //        {
    //            if(hex.Value.IsEmpty)
    //                GenerateResourceOnTile(hex.Value, resourceDistribution);
    //        }
    //    }

    //    private void GenerateResourceOnTile(Hex hex, ResourceDistributionModel resourceDistribution)
    //    {
    //        if (resourceDistribution.ResourceGenerationThresholdByBiome.ContainsKey(hex.Biom))
    //        {
    //            var resourceGenerationThreshold = resourceDistribution.ResourceGenerationThresholdByBiome[hex.Biom];
    //            if (random.NextDouble() < resourceGenerationThreshold.Threshold)
    //            {
    //                hex.Resource = rouletteWheelSelector.RouletteWheelSelection(resourceGenerationThreshold.ResourceGenerators);
    //            }
    //        }
    //    }
    //}
}
