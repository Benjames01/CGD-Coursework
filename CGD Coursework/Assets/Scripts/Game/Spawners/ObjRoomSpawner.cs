using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string mName;

        public SpawnerData mSpawnData;
    }

    [SerializeField]
    GridController mGrid;

    [SerializeField]
    RandomSpawner[] mSpawnData;

    void Start()
    {
    }

    void SpawnObjects(RandomSpawner data)
    {
        Debug.Log("Min" + data.mSpawnData.mMinimumSpawn);
        Debug.Log("Max" + data.mSpawnData.mMaximumSpawn);
        int randIteration = Random.Range(data.mSpawnData.mMinimumSpawn,
            data.mSpawnData.mMaximumSpawn + 1);

        for (int i = 0; i < randIteration; i++)
        {
            int randPos = Random.Range(0, mGrid.FreePoints.Count - 1);
            GameObject go = Instantiate(data.mSpawnData.mObjToSpawn,
                mGrid.FreePoints[randPos],
                Quaternion.identity, transform);
            mGrid.FreePoints.RemoveAt(randPos);
        }
    }

    public void InitObjectSpawning()
    {
        foreach (RandomSpawner spawner in mSpawnData)
        {
            SpawnObjects(spawner);
        }
    }
}
