using System;

public class MinMax
{
    public float Min { get; }
    public float Max { get; }

    public MinMax(float min, float max)
    {
        this.Min = min;
        this.Max = max;
    }

    public float CalculateRange()
    {
        return Max - Min;
    }
}