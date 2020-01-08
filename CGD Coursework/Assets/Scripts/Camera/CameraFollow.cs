using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform followTransform;

    [SerializeField]
    private float xBound = 0.15f;

    [SerializeField]
    private float yBound = 0.05f;

    public void Start()
    {
            followTransform = GameManager.instance.Player.transform;       
    }
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;


        // Check if inside the x axis boundary
        float xDelta = followTransform.position.x - transform.position.x;
        if (xDelta > xBound || xDelta < -xBound)
        {
            if(transform.position.x < followTransform.position.x)
            {
                delta.x = xDelta - xBound;
            } else
            {
                delta.x = xDelta + xBound;
            }
        }

        // Check if inside the y axis boundary
        float yDelta = followTransform.position.y - transform.position.y;
        if (yDelta > yBound || yDelta < -yBound)
        {
            if (transform.position.y < followTransform.position.y)
            {
                delta.y = yDelta - yBound;
            }
            else
            {
                delta.y = yDelta + yBound;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }



}
