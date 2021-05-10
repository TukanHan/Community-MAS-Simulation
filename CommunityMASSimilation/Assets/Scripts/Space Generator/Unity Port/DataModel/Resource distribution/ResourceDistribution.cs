using SpaceGeneration.DataModels;
using UnityEngine;

namespace SpaceGeneration.UnityPort
{
    public abstract class ResourceDistribution : ScriptableObject
    {
        public HexSO resource;

        [Range(0f, 1f)]
        public float threshold;

        public abstract ResourceDistributionModel ToModel();
    }
}