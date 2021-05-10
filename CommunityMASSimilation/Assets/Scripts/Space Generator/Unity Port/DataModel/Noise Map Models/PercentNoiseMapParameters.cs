using SpaceGeneration.DataModels;
using System;
using UnityEngine;

namespace SpaceGeneration.UnityPort
{
    [Serializable]
    public class PercentNoiseMapParameters : NoiseMapParameters
    {
        [Range(0, 1)]
        public float minPercent = 0;

        [Range(0, 1)]
        public float maxPercent = 1;

        public new PercentNoiseMapParametersModel ToModel()
        {
            return new PercentNoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency,
                PercentRange = new MinMax(minPercent, maxPercent)
            };
        }
    }
}
