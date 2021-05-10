using System.Collections.Generic;
using UnityEngine;

namespace Professions
{
    [CreateAssetMenu(menuName = "Simulation/Production Scheme/Prosessing")]
    public class ProcessingProductionScheme : WorkTask
    {
        public ResourceCountPair ingredient;

        public override List<ResourceCountPair> GetRequiredIngredience()
        {
            return new List<ResourceCountPair>()
            {
                ingredient
            };
        }
    }

}