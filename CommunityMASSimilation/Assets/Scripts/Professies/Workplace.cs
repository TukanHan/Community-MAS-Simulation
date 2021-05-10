using SpaceGeneration.DataModels;
using UnityEngine;

namespace Professions
{
    [CreateAssetMenu(menuName = "Simulation/Workplace")]
    public class Workplace : ScriptableObject
    {
        public string strName;
        public HexType workplaceHexType;
        public Sprite workplaceImage;
    }
}