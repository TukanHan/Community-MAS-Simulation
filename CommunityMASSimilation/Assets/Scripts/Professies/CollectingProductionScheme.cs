using System.Collections.Generic;
using UnityEngine;

namespace Professions
{
    [CreateAssetMenu(menuName = "Simulation/Production Scheme/Collecting")]
    public class CollectingProductionScheme : WorkTask
    {
        public override List<ResourceCountPair> GetRequiredIngredience()
        {
            return new List<ResourceCountPair>();
        }
    }
}