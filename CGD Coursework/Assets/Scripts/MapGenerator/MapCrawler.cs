using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCrawler
{

    Vector2Int Position { get; set; }

    public MapCrawler(Vector2Int startPosition)
    {
        Position = startPosition;
    }

    public Vector2Int Travel(Dictionary<Direction,Vector2Int> directionTravelMap)
    {
        Direction toTravel = (Direction) Random.Range(0, directionTravelMap.Count);
        Position += directionTravelMap[toTravel];

        return Position;
    } 
}
