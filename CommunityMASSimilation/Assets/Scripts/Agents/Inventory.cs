using System;
using System.Collections.Generic;

namespace Agents
{
    public class Inventory
    {
        public event EventHandler<Resource> ResourceSubstracted;
        public event EventHandler<Resource> ResourceAdded;

        private Dictionary<Resource, float> resources { get; } = new Dictionary<Resource, float>();

        public Dictionary<Resource, float>.KeyCollection GetItemsList()
        {
            return resources.Keys;
        }

        public int GetItemsCount()
        {
            return resources.Count;
        }

        public float GetResourceCount(Resource resource)
        {
            return resources.ContainsKey(resource) ? resources[resource] : 0;
        }

        public bool HasResource(Resource resource)
        {
            return HasResource(resource, 1);
        }

        public bool HasResource(ResourceCountPair pair)
        {
            return HasResource(pair.resource, pair.count);
        }

        public bool HasResource(Resource resource, float count)
        {
            return (resources.ContainsKey(resource) && resources[resource] >= count);
        }

        public void AddResource(ResourceCountPair pair)
        {
            AddResource(pair.resource, pair.count);
        }

        public void AddResource(Resource resource, float count)
        {
            if (resources.ContainsKey(resource))
                resources[resource] += count;
            else
                resources[resource] = count;

            OnResourceAdded(resource);
        }

        private void OnResourceAdded(Resource resource)
        {
            if (ResourceAdded != null)
                ResourceAdded(this, resource);
        }

        public void SubtractResource(Resource resource)
        {
            SubtractResource(resource, 1);
        }

        public void SubtractResource(ResourceCountPair pair)
        {
            SubtractResource(pair.resource, pair.count);
        }

        public void SubtractResource(Resource resource, float count)
        {
            if (resources.ContainsKey(resource))
                resources[resource] -= count;
            else
                throw new InvalidOperationException($"Res: {resource} Count: {count}");

            if (resources[resource] == 0)
                resources.Remove(resource);

            OnSubtractResource(resource);
        }

        private void OnSubtractResource(Resource resource)
        {
            if (ResourceSubstracted != null)
                ResourceSubstracted(this, resource);
        }
    }
}