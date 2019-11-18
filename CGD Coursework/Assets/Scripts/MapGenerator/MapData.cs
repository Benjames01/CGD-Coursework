using UnityEngine;


[CreateAssetMenu(fileName = "MapGeneratorData.asset", menuName = "MapGenerationData/Map Data")]

public class MapData : ScriptableObject
{
    [SerializeField]
    int mCrawlerCount;

    [SerializeField]
    int mMinIterations;
    [SerializeField]
    int mMaxIterations;

    public int CrawlerCount { get => mCrawlerCount; set => mCrawlerCount = value; }

    public int MinIterations { get => mMinIterations; set => mMinIterations = value; }
    public int MaxIterations { get => mMaxIterations; set => mMaxIterations = value; }

}
