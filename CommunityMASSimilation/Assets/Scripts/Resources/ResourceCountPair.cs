using System;

[Serializable]
public class ResourceCountPair
{
    public Resource resource;
    public float count;

    public ResourceCountPair(Resource resource, float count)
    {
        this.resource = resource;
        this.count = count;
    }

    public ResourceCountPair(ResourceCountPair resourceCountPair)
    {
        this.resource = resourceCountPair.resource;
        this.count = resourceCountPair.count;
    }
}