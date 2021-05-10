using Coordinates;
using System.Collections.Generic;

namespace SpaceGeneration.Generation
{
    public class NoiseMapNormalizer
    {
        public Dictionary<CubeCoordinate, float> NormalizeNoiseMap(Dictionary<CubeCoordinate, float> noiseMap, float min = 0, float max = 1)
        {
            FindMinMax(noiseMap, out float minValue, out float maxValue);

            var keys = new List<CubeCoordinate>(noiseMap.Keys);
            foreach (var key in keys)
            {
                noiseMap[key] = ScaleValue(noiseMap[key], minValue, maxValue, min, max);
            }

            return noiseMap;
        }

        private void FindMinMax(Dictionary<CubeCoordinate, float> noiseMap, out float min, out float max)
        {
            min = float.MaxValue; max = float.MinValue;

            foreach (float value in noiseMap.Values)
            {
                if (value > max)
                    max = value;
                if (value < min)
                    min = value;
            }
        }

        private float ScaleValue(float value, float minValue, float maxValue, float newMin, float newMax)
        {
            float result = ((value - minValue) * (newMax - newMin) / (maxValue - minValue) + newMin);
            return Clamp(result, 0.0000001f, 0.9999999f);
        }

        private float Clamp(float value, float min, float max)
        {
            return value > max ? max : (value < min ? min : value);
        }
    }
}
