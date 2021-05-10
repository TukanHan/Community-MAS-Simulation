using Coordinates;
using System;
using System.Collections.Generic;

namespace SpaceGeneration.Generation
{
    public class NoiseMapTheresholdSelector
    {
        public float Thereshold { get; set; } = 0.5f;

        public float SelectThereshold(Dictionary<CubeCoordinate, float> noiseMap, float randomValue)
        {
            return SelectThereshold(noiseMap, new[] { randomValue }, (x => true))[0];
        }

        public float[] SelectThereshold(Dictionary<CubeCoordinate, float> noiseMap, float[] randomValues)
        {
            return SelectThereshold(noiseMap, randomValues, (x => true));
        }

        public float SelectThereshold(Dictionary<CubeCoordinate, float> noiseMap, float randomValue, Predicate<CubeCoordinate> predicate)
        {
            return SelectThereshold(noiseMap, new[] { randomValue }, predicate)[0];
        }

        public float[] SelectThereshold(Dictionary<CubeCoordinate, float> noiseMap, float[] randomValues, Predicate<CubeCoordinate> predicate)
        {
            List<float> values = GetAllValues(noiseMap, predicate);

            float[] theresholds = new float[randomValues.Length];
            for(int i=0; i< randomValues.Length; ++i)
            {
                int valuesCount = (int)(randomValues[i] * values.Count);
                theresholds[i] = (valuesCount > 0) ? values[valuesCount - 1] : 1;
            }
            
            return theresholds;
        }

        private List<float> GetAllValues(Dictionary<CubeCoordinate, float> noiseMap, Predicate<CubeCoordinate> predicate)
        {
            List<float> values = new List<float>();

            foreach(var noise in noiseMap)
            {
                if (predicate(noise.Key))
                    values.Add(noise.Value);
            }       

            values.Sort(new Comparison<float>( (a, b) => b.CompareTo(a)));
            return values;
        }
    }
}
