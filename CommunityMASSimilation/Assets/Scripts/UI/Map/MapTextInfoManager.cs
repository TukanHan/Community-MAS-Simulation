using Agents.WorkplaceSelection;
using Coordinates;
using SpaceGeneration.UnityPort;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.WorkplacePreference;
using UnityEngine;

public class MapTextInfoManager : SingletonBehaviour<MapTextInfoManager>
{
    public MapTextInfo prefab;

    private Dictionary<CubeCoordinate, MapTextInfo> informations = new Dictionary<CubeCoordinate, MapTextInfo>();
    private Stack<MapTextInfo> textInfos = new Stack<MapTextInfo>();

    private void Start()
    {
        foreach(var key in SpaceGenerator.instance.Map.Keys)
        {
            informations[key] = InstantinePrefab(key);
        }
    }

    public void EnableMapInfo(WorkplacePreferenceMap map)
    {
        DisableMapInfo();

        MarkLocation(map.AgentLocation);
        MarkWorkplaceMap(map.DistanceMap);
        MarkSupplayers(map.Supplayers);
    }

    private void MarkLocation(CubeCoordinate? location)
    {
        if(location.HasValue)
        {
            MapTextInfo info = informations[location.Value];
            info.EnableFrame();
            textInfos.Push(info);
        }
    }

    private void MarkWorkplaceMap(DistanceMap map)
    {
        if(map != null)
        {
            float max = map.Values.Max();
            float min = map.Values.Min();

            foreach (CubeCoordinate coordinate in map.Keys)
            {
                MapTextInfo info = informations[coordinate];
                info.gameObject.SetActive(true);
                info.EnableColorFilter(String.Format("{0:0.##}", map[coordinate]), CalculatePercent(min, max, map[coordinate]));
                textInfos.Push(info);
            }
        }
    }

    private void MarkSupplayers(List<Tuple<CubeCoordinate, Resource>> supplayers)
    {
        if(supplayers != null)
        {
            foreach (var tuple in supplayers)
            {
                MapTextInfo info = informations[tuple.Item1];
                info.EnableResourceSuplyerInfo(tuple.Item2);
                textInfos.Push(info);
            }
        }
    }

    public void DisableMapInfo()
    {
        while(textInfos.Count > 0)
        {
            textInfos.Pop().Disable();
        }
    }

    public float CalculatePercent(float min, float max, float value)
    {
        float r = (max - min);
        if(r == 0)
            return 0.5f;

        return (value - min) / r;
    }

    private MapTextInfo InstantinePrefab(CubeCoordinate cubeCoordinate)
    {
        GameObject go = Instantiate(prefab.gameObject, SpaceGenerator.instance.Map[cubeCoordinate].hexObject.transform);
        return go.GetComponent<MapTextInfo>();
    }
}