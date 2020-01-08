using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{

    // Damage
    [SerializeField]
    private int damage;
    [SerializeField]
    private float pushForce;

    public int Damage { get => damage; set => damage = value; }
    public float PushForce { get => pushForce; set => pushForce = value; }

    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Fighter" && col.name == "Player")
        {
            // Create new damage object before sending to player
            Damage dmg = new Damage
            {
                DamageAmount = damage,
                Origin = transform.position,
                PushForce = pushForce
            };

            col.SendMessage("ReceiveDamage", dmg);
        }
    }
}
