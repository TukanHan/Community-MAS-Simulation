using UnityEngine;

namespace SpaceGeneration.DataModels
{
    [CreateAssetMenu(menuName = "Simulation/Hex Type")]
    public class HexSO : ScriptableObject
    {
        public HexType hexType;
        public Sprite image;
        public string strName;
    }
}