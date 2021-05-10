using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceGeneration.UnityPort
{
    [CreateAssetMenu(menuName = "Simulation/Resource Distibution/Random")]
    public class RandomResourceDistribution : ResourceDistribution
    {
        public override DataModels.ResourceDistributionModel ToModel()
        {
            return new DataModels.RandomResourceDistributionModel
            {
                Resource = resource
            };
        }
    }
}
