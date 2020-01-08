using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    [SerializeField]
    private int healAmount = 1;
    public int HealAmount { get => healAmount; set => healAmount = value; }

    private float healCooldown;
    private float lastHeal;

    protected override void OnCollide(Collider2D col)
    {
        if(Time.time - lastHeal > healCooldown && col.name == "Player" && col.tag == "Fighter")
        {
            lastHeal = Time.time;
            GameManager.instance.Player.Heal(healAmount);
        }
    }
}
