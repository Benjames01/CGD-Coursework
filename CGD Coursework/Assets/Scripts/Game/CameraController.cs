using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    Area mCurrentArea;

    [SerializeField]
    float mCameraSpeed;

    public Area CurrentArea { get => mCurrentArea; set => mCurrentArea = value; }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if(mCurrentArea == null)
            return;

        Vector3 targetPosition = GetTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * mCameraSpeed);
    }

    private Vector3 GetTargetPosition()
    {
        if (mCurrentArea == null)
            return Vector3.zero;

        Vector3 targetPosition = mCurrentArea.GetAreaCentre();
        targetPosition.z = transform.position.z;

        return targetPosition;
    }


    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetTargetPosition()) == false;
    }
}
