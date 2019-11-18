using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    MapData mMapData;

    List<Vector2Int> mMapAreas;

    void Start()
    {
        mMapAreas = MapCrawlerController.GenerateMap(mMapData);
        GenerateAreas(mMapAreas);
    }

    void GenerateAreas(IEnumerable<Vector2Int> areas)
    {
        AreaController.instance.LoadArea("Start", 0, 0);

        foreach(Vector2Int areaLocation in areas)
        { 
                AreaController.instance.LoadArea("Empty", areaLocation.x, areaLocation.y);         
        }
    }
}
