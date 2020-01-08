using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage
{

    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private int damageAmount;
    [SerializeField]
    private float pushForce;

    public Vector3 Origin { get => origin; set => origin = value; }
    public int DamageAmount { get => damageAmount; set => damageAmount = value; }
    public float PushForce { get => pushForce; set => pushForce = value; }
}
