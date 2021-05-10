using UnityEngine;

[CreateAssetMenu(menuName = "Simulation/Resource")]
public class Resource : ScriptableObject
{
    public string resName;
    public ResourceTag tag;
    public Sprite image;
}
