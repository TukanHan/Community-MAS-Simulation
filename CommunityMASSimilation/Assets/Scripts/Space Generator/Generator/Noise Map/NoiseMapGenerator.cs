using Coordinates;
using SpaceGeneration.DataModels;
using System;
using System.Collections.Generic;

namespace SpaceGeneration.Generation
{
    public class NoiseMapGenerator
    {
        private float xOff;
        private float yOff;

        private Random random;

        public NoiseMapGenerator(Random random)
        {
            this.random = random;
        }

        public Dictionary<CubeCoordinate, float> Generate(NoiseMapParametersModel noiseMapParameters, HexMap hexMap)
        {
            xOff = random.Next(0, 1000000);
            yOff = random.Next(0, 1000000);

            Dictionary<CubeCoordinate, float> map = new Dictionary<CubeCoordinate, float>();

            foreach(var pos in hexMap.Map.Keys)
            {
                map[pos] = Clamp(CalculateValue(noiseMapParameters, CalculateHexPosition(new Coordinate(pos))), 0, 0.999f);
            }

            return map;
        }

        private float Clamp(float value, float min, float max)
        {
            return value > max ? max : (value < min ? min : value);
        }

        private float CalculateValue(NoiseMapParametersModel parameters, Vector2Float position)
        {
            float noise = 0.0f;
            float gain = 1.0f;
            for (int i = 0; i < parameters.Octaves; ++i)
            {
                float value = ImprovedNoise.Noise2D((position.X + xOff) * gain / parameters.Frequency, (position.Y + yOff) * gain / parameters.Frequency);
                noise += value * 0.5f / gain;
                gain *= 2;
            }

            return noise;
        }

        private Vector2Float CalculateHexPosition(Coordinate postion)
        {
            return new Vector2Float(postion.Y % 2 == 0 ? postion.X : postion.X - 0.5f, postion.Y * 0.875f);
        }
    }
}