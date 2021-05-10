using System;
using System.Collections.Generic;
using UnityEngine;

namespace Professions
{
    public abstract class WorkTask : ScriptableObject
    {
        public Workplace profession;
        public string productionName;
        public ResourceCountPair product;
        public uint time;

        public abstract List<ResourceCountPair> GetRequiredIngredience();
    }

    [Serializable]
    public class ListOfSchemes : SerializableList<WorkTask> { }
}