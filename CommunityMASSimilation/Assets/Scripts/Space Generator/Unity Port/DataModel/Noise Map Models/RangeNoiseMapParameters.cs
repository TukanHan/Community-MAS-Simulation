using SpaceGeneration.DataModels;
using System;
using UnityEngine;

namespace SpaceGeneration.UnityPort
{
    [Serializable]
    public class RangeNoiseMapParameters : NoiseMapParameters
    {
        [Range(0, 1)]
        public float minValue = 0;

        [Range(0, 1)]
        public float maxValue = 1;

        public new RangeNoiseMapParametersModel ToModel()
        {
            return new RangeNoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency,
                Range = new MinMax(minValue, maxValue)
            };
        }
    }
}
