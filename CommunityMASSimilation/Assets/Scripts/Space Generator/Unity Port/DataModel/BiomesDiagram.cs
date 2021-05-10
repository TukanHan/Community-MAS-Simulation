using SpaceGeneration.DataModels;
using System;

namespace SpaceGeneration.UnityPort
{
    [Serializable]
    public class BiomesDiagram
    {
        public const int MaxXLayerCount = 3;
        public const int MaxYLayerCount = 3;

        public int AxiXLayerCount = 2;
        public int AxiYLayerCount = 3;

        public HexSO[] biomes = new HexSO[MaxXLayerCount * MaxYLayerCount];

        public HexSO this[int i, int j]
        {
            get { return biomes[i * MaxYLayerCount + j]; }
            set { biomes[i * MaxYLayerCount + j] = value; }
        }

        public HexSO[,] ToModel()
        {
            HexSO[,] biomArray = new HexSO[AxiYLayerCount, AxiXLayerCount];

            for (int i = 0; i < AxiYLayerCount; i++)
            {
                for (int j = 0; j < AxiXLayerCount; j++)
                {
                    biomArray[i, j] = this[i,j];
                }
            }

            return biomArray;
        }
    }
}
