using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    ExitType mExitType;

    public ExitType Exittype{ get => mExitType; set => mExitType = value; }

}

public enum ExitType
{
    up,
    down,
    left,
    right
}
