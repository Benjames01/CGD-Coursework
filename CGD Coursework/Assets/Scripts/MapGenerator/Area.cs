using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Area : MonoBehaviour
{

    [SerializeField]
    int mWidth;
    [SerializeField]
    int mHeight;

    [SerializeField]
    int mX;
    [SerializeField]
    int mY;

    bool mUpdatedExits = false;

    public int Width { get => mWidth; set => mWidth = value; }
    public int Height { get => mHeight; set => mHeight = value; }

    public int X { get => mX; set => mX = value; }
    public int Y { get => mY; set => mY = value; }

    Exit mLeftExit, mRightExit, mUpExit, mDownExit;

    [SerializeField]
    List<Exit> mExits = new List<Exit>();

    public Area(int x, int y)
    {
        mX = x;
        mY = y;
    }


    void Update()
    {
        if (name.Contains("Finish") && !mUpdatedExits)
        {
            RemoveDisconnectedExits();
            mUpdatedExits = true;
        }
    }

    void Start()
    {
        if (AreaController.instance == null)
        {
            Debug.Log("ERROR: Started game in wrong scene, scene must contain AreaController");
            return;
        }

        Exit[] exts = GetComponentsInChildren<Exit>();

        if (exts.Length != 0)
        {
            foreach (Exit e in exts)
            {
                mExits.Add(e);
                switch (e.Exittype)
                {
                    case ExitType.down:
                        mDownExit = e;
                        break;
                    case ExitType.up:
                        mUpExit = e;
                        break;
                    case ExitType.left:
                        mLeftExit = e;
                        break;
                    case ExitType.right:
                        mRightExit = e;
                        break;
                }
            }
        }

        AreaController.instance.RegisterArea(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(mWidth, mHeight));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AreaController.instance.OnPlayerEnterArea(this);
        }
    }

    Area GetArea(int x, int y)
    {
        if (AreaController.instance.DoesAreaExist(x, y))
        {
            return AreaController.instance.FindArea(x, y);
        }
        return null;
    }

    private void DisableExitAndEnableCollider(Exit exit)
    {
        if (exit == null)
            return;

        exit.GetComponent<SpriteRenderer>().enabled = false;
        exit.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public void RemoveDisconnectedExits()
    {
        foreach (Exit e in mExits)
        {
            switch (e.Exittype)
            {
                case ExitType.down:
                    if (GetArea(mX, mY - 1) == null)
                        DisableExitAndEnableCollider(e);
                    break;
                case ExitType.up:
                    if (GetArea(mX, mY + 1) == null)
                        DisableExitAndEnableCollider(e);
                    break;
                case ExitType.left:
                    if (GetArea(mX - 1, mY) == null)
                        DisableExitAndEnableCollider(e);
                    break;
                case ExitType.right:
                    if (GetArea(mX + 1, mY) == null)
                        DisableExitAndEnableCollider(e);
                    break;
            }
        }
    }

    public Vector3 GetAreaCentre()
    {
        return new Vector3(mX * mWidth, mY * mHeight);
    }
}
