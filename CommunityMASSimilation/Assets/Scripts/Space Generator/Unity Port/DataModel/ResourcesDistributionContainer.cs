using SpaceGeneration.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceGeneration.UnityPort
{
    [Serializable]
    public class ResourcesContainer
    {
        public List<ResourceDistribution> resources = new List<ResourceDistribution>();

        public void AddResource()
        {
            resources.Add(null);
        }

        public ResourceDistributionModel[] ToModel()
        {
            return resources.Select(x => x.ToModel()).ToArray();
        }
    }
}
