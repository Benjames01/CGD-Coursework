using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class AreaController : MonoBehaviour
{

    public static AreaController instance; // Used for Singleton pattern 

    string mCurrentDungeonName = "Ground";

    AreaData mCurrentLoadAreaData;

    Area mCurrentArea;

    Queue<AreaData> mLoadAreaQueue = new Queue<AreaData>();

    [SerializeField]
    List<Area> mLoadedAreas = new List<Area>();

    bool mIsLoadingArea = false;
    bool mSpawnedFinishArea = false;
    bool mUpdatedAreas = false;

    public List<Area> LoadedAreas { get => mLoadedAreas; set => mLoadedAreas = value; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        UpdateAreaQueue();
    }

    void UpdateAreaQueue()
    {
        if (mIsLoadingArea)
            return;

        if (mLoadAreaQueue.Count == 0)
        {
            if (!mSpawnedFinishArea)
            {
                StartCoroutine(SpawnFinishArea());
            } else if (mSpawnedFinishArea && !mUpdatedAreas)
            {
                foreach(Area area in mLoadedAreas)
                {
                    area.RemoveDisconnectedExits();
                }
                mUpdatedAreas = true;
            }
            return;
        }

        mCurrentLoadAreaData = mLoadAreaQueue.Dequeue();
        mIsLoadingArea = true;

        StartCoroutine(LoadAreaRoutine(mCurrentLoadAreaData));
    }

    IEnumerator LoadAreaRoutine(AreaData data)
    {
        string areaName = mCurrentDungeonName + data.Name;

        AsyncOperation loadArea = SceneManager.LoadSceneAsync(areaName, LoadSceneMode.Additive);

        while (!loadArea.isDone)
            yield return null;
        
    }

    IEnumerator SpawnFinishArea()
    {
        mSpawnedFinishArea = true;
        yield return new WaitForSeconds(0.5f);
        if (mLoadAreaQueue.Count == 0)
        {
            Area finishArea = mLoadedAreas[mLoadedAreas.Count - 1];
            Area tempArea = new Area(finishArea.X, finishArea.Y);

            Destroy(finishArea.gameObject);

            var removeArea = mLoadedAreas.Single(rem => rem.X == tempArea.X && rem.Y == tempArea.Y);
            mLoadedAreas.Remove(removeArea);
            LoadArea("Finish", tempArea.X, tempArea.Y);
        }
    }

    internal Area FindArea(int x, int y)
    {
        return mLoadedAreas.Find(area => area.X == x && area.Y == y);
    }

    public void LoadArea(string name, int x, int y)
    {
        if (DoesAreaExist(x, y))
            return;

        AreaData newAreaData = new AreaData();
        newAreaData.Name = name;
        newAreaData.X = x;
        newAreaData.Y = y;

        mLoadAreaQueue.Enqueue(newAreaData);
    }

    public void RegisterArea(Area area)
    {

        if(DoesAreaExist(mCurrentLoadAreaData.X, mCurrentLoadAreaData.Y))
        {
            Destroy(area.gameObject);
            mIsLoadingArea = false;
            return;
        }


        area.transform.position = new Vector3(
            mCurrentLoadAreaData.X * area.Width,
            mCurrentLoadAreaData.Y * area.Height,
            0);

        area.X = mCurrentLoadAreaData.X;
        area.Y = mCurrentLoadAreaData.Y;
        area.name = mCurrentDungeonName + "-" +
            mCurrentLoadAreaData.Name +
            " (" + area.X + ", " + area.Y + ")";
        area.transform.parent = transform;

        mIsLoadingArea = false;

        if (mLoadedAreas.Count == 0)
            CameraController.instance.CurrentArea = area;

        mLoadedAreas.Add(area);       
    }

    public bool DoesAreaExist(int x, int y)
    {
        return LoadedAreas.Find(area => area.X == x && area.Y == y);
    }

    public void OnPlayerEnterArea(Area area)
    {
        CameraController.instance.CurrentArea = area;
        mCurrentArea = area;
    }
}
