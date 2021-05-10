using Coordinates;
using UnityEngine;

namespace Agents.Needs
{
    public static class EnergyConsumptionCalculator
    {
        public const float MinEnergyCost = 0.00001f;
        public const float MaxEnergyCost = 0.06f;

        public const float HexTravelEnergyCost = 0.003f;

        public static float CalculateEnergyReducingValue(float satisfaction)
        {
            return Mathf.Lerp(MinEnergyCost, MaxEnergyCost, satisfaction);          
        }

        public static float ConsumeTravelEnergy(CubeCoordinate locationA, CubeCoordinate locationB)
        {
            return CubeCooridinatesManager.CalculateDistance(locationA, locationB) * HexTravelEnergyCost;
        }
    }
}