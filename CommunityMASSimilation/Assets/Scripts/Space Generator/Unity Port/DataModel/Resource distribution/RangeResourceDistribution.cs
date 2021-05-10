using SpaceGeneration.DataModels;
using UnityEngine;

namespace SpaceGeneration.UnityPort
{
    [CreateAssetMenu(menuName = "Simulation/Resource Distibution/Range")]
    public class RangeResourceDistribution : ResourceDistribution
    {
        [Range(1,5)]
        public int minRange;

        public override ResourceDistributionModel ToModel()
        {
            return new RangeResourceDistributionModel
            {
                Resource = resource,
                Threshold = threshold,
                Range = minRange
            };
        }
    }
}
