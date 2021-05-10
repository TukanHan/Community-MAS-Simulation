using System;

namespace SpaceGeneration.DataModels
{
    public enum HexType
    {
        Plain,
        Forest,
        Mine,
        Water,
        Farmland
    }

    public static class HexTypeExtension
    {
        public static string GetHexTypeName(this HexType hexType)
        {
            switch(hexType)
            {
                case HexType.Plain:
                    return "Równina";
                case HexType.Forest:
                    return "Las";
                case HexType.Mine:
                    return "Równina";
                case HexType.Water:
                    return "Water";
                case HexType.Farmland:
                    return "Farma";
                default:
                    throw new NotImplementedException(hexType.ToString());
            }
        }
    }
}