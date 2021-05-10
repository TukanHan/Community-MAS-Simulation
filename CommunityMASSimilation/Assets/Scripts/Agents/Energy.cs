using System;

namespace Agents
{
    public class Energy
    {
        public const float MeetingNeedsThreshold = 0.5f;

        public const int FoodConsumptionPriority = 3;
        public const int BaseConsumptionPriority = 2;
        public const int LuxuryConsumptionPriority = 1;

        private const float ConsumptionPrioritySum = FoodConsumptionPriority + BaseConsumptionPriority + LuxuryConsumptionPriority;

        public const float FoodConsumptionFactor = FoodConsumptionPriority / ConsumptionPrioritySum;
        public const float BaseConsumptionFactor = BaseConsumptionPriority / ConsumptionPrioritySum;
        public const float LuxuryConsumptionFactor = LuxuryConsumptionPriority / ConsumptionPrioritySum;

        public bool IsAlive 
        { 
            get { return true; }
        }

        public float FoodNeeds { get; private set; } = 1f;
        public bool AreFoodNeedsMeeting { get { return FoodNeeds >= MeetingNeedsThreshold; } }
        public float BasicNeeds { get; private set; } = 1f;
        public bool AreBasicNeedsMeeting { get { return AreFoodNeedsMeeting && BasicNeeds >= MeetingNeedsThreshold; } }
        public float LuxuryNeeds { get; private set; } = 1f;
        public bool AreLuxuryNeedsMeeting { get { return AreBasicNeedsMeeting && LuxuryNeeds >= MeetingNeedsThreshold; } }

        public float CalculateSatisfaction()
        {
            float energyLevel = FoodNeeds;

            if (AreFoodNeedsMeeting)
                energyLevel += BasicNeeds;

            if (AreBasicNeedsMeeting)
                energyLevel += LuxuryNeeds;

            return energyLevel / 3;
        }

        #region Energy compsumption
        public void ConsumeEnergy(float energy)
        {
            ConsumeLuxuryEnergy(energy * LuxuryConsumptionFactor);
            ConsumeBasicEnergy(energy * BaseConsumptionFactor);
            ConsumeFoodEnergy(energy * FoodConsumptionFactor);
        }

        private void ConsumeLuxuryEnergy(float energy)
        {
            if(energy > LuxuryNeeds)
            {
                ConsumeBasicEnergy(energy - LuxuryNeeds);
                LuxuryNeeds = 0;
            }
            else
            {
                LuxuryNeeds -= energy;
            }
        }

        private void ConsumeBasicEnergy(float energy)
        {
            if (energy > BasicNeeds)
            {
                ConsumeFoodEnergy(energy - BasicNeeds);
                LuxuryNeeds = 0;
            }
            else
            {
                BasicNeeds -= energy;
            }
        }

        private void ConsumeFoodEnergy(float energy)
        {
            FoodNeeds -= energy;
            if (FoodNeeds < 0)
                FoodNeeds = 0;
        }
        #endregion

        #region Energy Regeneration
        public void AddFoodEnergy(float energy)
        {
            FoodNeeds += energy;
            if (FoodNeeds > 1)
                FoodNeeds = 1;
        }

        public void AddBasicEnergy(float energy)
        {
            BasicNeeds += energy;
            if (BasicNeeds > 1)
                BasicNeeds = 1;
        }

        public void AddLuxuryEnergy(float energy)
        {
            LuxuryNeeds += energy;
            if (LuxuryNeeds > 1)
                LuxuryNeeds = 1;
        }
        #endregion
    }
}