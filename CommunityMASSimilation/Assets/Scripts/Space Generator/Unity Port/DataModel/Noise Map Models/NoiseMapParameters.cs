using System;
using UnityEngine;

namespace SpaceGeneration.UnityPort
{
    [Serializable]
    public class NoiseMapParameters
    {
        [Range(1,8)]
        public int octaves = 5;

        [Range(1,50)]
        public float frequency = 30;
        
        public DataModels.NoiseMapParametersModel ToModel()
        {
            return new DataModels.NoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency
            };
        }
    }
}
