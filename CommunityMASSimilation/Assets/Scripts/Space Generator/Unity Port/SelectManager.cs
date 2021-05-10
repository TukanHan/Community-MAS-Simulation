using Coordinates;
using SpaceGeneration.UnityPort;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : SingletonBehaviour<SelectManager>
{
    public Sprite frame;
    public Color EnabledColor;
    public Color DisabledColor;

    private List<GameObject> currentFrames = new List<GameObject>();

    public void Select()
    {
        foreach (var hex in SpaceGenerator.instance.Map)
        {
            Coordinate offsetCoords = new Coordinate(hex.Key);
            Vector2 position = new Vector2(offsetCoords.Y % 2 == 0 ? offsetCoords.X : offsetCoords.X - 0.5f, offsetCoords.Y * 0.875f);

            currentFrames.Add(CreateFrame(frame, position, transform, EnabledColor));
        }
    }

    public void Clear()
    {
        foreach(GameObject go in currentFrames)
        {
            Destroy(go);
        }
        currentFrames.Clear();
    }


    private GameObject CreateFrame(Sprite image, Vector2 pos, Transform parent, Color color)
    {
        GameObject sprite = new GameObject();

        sprite.transform.parent = parent;
        sprite.transform.position = pos;

        SpriteRenderer spriteRenderer = sprite.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.color = color;

        return sprite;
    }
}
