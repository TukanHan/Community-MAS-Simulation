using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceGeneration.UnityPort
{
    [CreateAssetMenu(menuName = "Simulation/Resource Distibution/Noise Map")]
    public class NoiseMapResourceDistribution : ResourceDistribution
    {
        public PercentNoiseMapParameters noiseMapParameters;

        public override DataModels.ResourceDistributionModel ToModel()
        {
            return new DataModels.NoiseMapResourceDistributionModel
            {
                Resource = resource,
                Threshold = threshold,
                NoiseMapParametersModel = noiseMapParameters.ToModel()
            };
        }
    }
}
