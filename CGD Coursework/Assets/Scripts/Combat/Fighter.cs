using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    [SerializeField]
    private int hitpoints = 10;
    [SerializeField]
    private int maxHitpoints = 10;
    [SerializeField]
    private float pushRecoverySpeed = 0.2f;

    public int Hitpoints { get => hitpoints; set => hitpoints = value; }
    public int MaxHitpoints { get => maxHitpoints; set => maxHitpoints = value; }
    public float PushRecoverySpeed { get => pushRecoverySpeed; set => pushRecoverySpeed = value; }

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push 
    protected Vector3 pushDirection;

    // All fighters can ReceiveDamage
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoints -= dmg.DamageAmount;
            pushDirection = (transform.position - dmg.Origin).normalized * dmg.PushForce;

            GameManager.instance.ShowText("-" + dmg.DamageAmount.ToString(), 15, Color.red, transform.position, Vector3.up * 50, 0.5f);

            if(hitpoints <= 0)
            {
                hitpoints = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }

}
