using SpaceGeneration.DataModels;

namespace SpaceGeneration.DataModels
{
    public class Hex
    {
        public HexSO HexModel { get; set; }

        public float WaterDeepness { get; set; }
        public float Temperature { get; set; }
        public float Height { get; set; }
        public float AuxiliaryValue { get; set; }
        public uint MountainHeight { get; set; }
    }
}