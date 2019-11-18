using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField]
    Area area;


    [System.Serializable] // Needs to be serialisable in order to edit in Unity Editor
    public struct Grid
    {
        public int mCol, mRow;
        public float mVOffset, mHOffset;
    }

    [SerializeField]
    Grid grid;

    [SerializeField]
    GameObject gridTile;

    [SerializeField]
    List<Vector2> freePoints = new List<Vector2>();

    public List<Vector2> FreePoints { get => freePoints; }

    void Awake()
    {
        area = GetComponentInParent<Area>();
        grid.mCol = area.Width - 2;
        grid.mRow = area.Height - 2;
        grid.mHOffset = 8.5f;
        grid.mVOffset = 4.5f;

        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid.mVOffset += area.transform.localPosition.y;
        grid.mHOffset += area.transform.localPosition.x;

        for (int i = 0; i < grid.mRow; i++)
        {
            for (int y = 0; y < grid.mCol; y++)
            {
                if (gridTile == null)
                    break;

                GameObject tile = Instantiate(gridTile, transform);

                tile.transform.position =
                    new Vector2(y - (grid.mCol - grid.mHOffset),
                    i - (grid.mRow - grid.mVOffset));
                tile.name = "(" + y + "," + i + ")";

                freePoints.Add(tile.transform.position);
            }
        }

        if(GetComponentInParent<ObjRoomSpawner>() != null)
            GetComponentInParent<ObjRoomSpawner>().InitObjectSpawning();
    }

}
