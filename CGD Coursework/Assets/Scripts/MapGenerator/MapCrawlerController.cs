using System.Collections.Generic;
using UnityEngine;

public class MapCrawlerController : MonoBehaviour
{

    public static List<Vector2Int> mPositionTraversed = new List<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int> mDirectionTravelMap =
        new Dictionary<Direction, Vector2Int>()
        {
            {Direction.up, Vector2Int.up},
            {Direction.down, Vector2Int.down},
            {Direction.left, Vector2Int.left},
            {Direction.right, Vector2Int.right}
        };

    public static List<Vector2Int> GenerateMap(MapData data)
    {
        List<MapCrawler> mapCrawlers = new List<MapCrawler>();

        for(int i = 0; i < data.CrawlerCount; i++)
        {
            mapCrawlers.Add(new MapCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(data.MinIterations, data.MaxIterations);

        for(int x = 0; x < iterations; x++)
        {
            foreach (MapCrawler crawler in mapCrawlers)
            {
                Vector2Int newPosition = crawler.Travel(mDirectionTravelMap);
                mPositionTraversed.Add(newPosition);
            }
        }
        return mPositionTraversed;
    }
}

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
}
