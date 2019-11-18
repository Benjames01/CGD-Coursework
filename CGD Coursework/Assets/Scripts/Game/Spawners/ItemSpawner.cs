using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField]
    List<Spawnable> mItems = new List<Spawnable>();

    float mTotalWeight;

    void Awake()
    {
        mTotalWeight = 0f;
        foreach (var spawnable in mItems)
        {
            mTotalWeight += spawnable.mWeight;
        }
    }

    void Start()
    {
        float rand = Random.value * mTotalWeight;
        float cumulativeWeight = mItems[0].mWeight;

        int index = 0;

        while (rand > cumulativeWeight && rand < mItems.Count - 1)
        {
            index++;
            cumulativeWeight += mItems[index].mWeight;
        }


        GameObject item = Instantiate(mItems[index].mGameObject, transform.position, Quaternion.identity, transform) as GameObject;
    }


    [System.Serializable]
    public struct Spawnable
    {
        public GameObject mGameObject;
        public float mWeight;
    }
}

