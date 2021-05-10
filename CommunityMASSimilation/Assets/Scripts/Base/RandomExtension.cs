using System;
using System.Collections.Generic;

public static class RandomExtension
{
    public static T RandomElement<T>(this List<T> list, Random random)
    {
        return list[random.Next(0, list.Count)];
    }

    public static float RandomFloat(this Random random, float min, float max)
    {
        return(float)((max - min) * random.NextDouble()) + min;
    }

    public static float RandomFloat(this Random random, MinMax range)
    {
        return random.RandomFloat(range.Min, range.Max);
    }

    public static bool RandomBool(this Random random)
    {
        return random.Next(0, 2) == 1 ? true : false;
    }

    public static bool RandomThereshold(this Random random, float thereshold)
    {
        return random.NextDouble() < thereshold;
    }
}