using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{

    private bool alive = true;

    public bool Alive { get => alive; set => alive = value; }

    protected override void Death()
    {
        Alive = false;
        GameManager.instance.DeathMenuAnim.SetTrigger("Show");

    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!Alive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        if(Alive)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void OnLevelUp()
    {
        MaxHitpoints++;
        Hitpoints = MaxHitpoints;
        GameManager.instance.OnHitpointChange();
        GetComponentInChildren<MagicAttack>().UpgradeWeapon();
    }

    public void Heal(int healAmount)
    {

        if (Hitpoints == MaxHitpoints)
            return;

        Hitpoints += healAmount;

        if (Hitpoints > MaxHitpoints)
            Hitpoints = MaxHitpoints;

        GameManager.instance.OnHitpointChange();
        GameManager.instance.ShowText("+" + healAmount.ToString() + " hp!", 15, Color.green, transform.position, Vector3.up * 30, 1f);           
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    public void Respawn()
    {
        lastImmune = Time.time;
        pushDirection = Vector3.zero;

        alive = true;
        Heal(MaxHitpoints);
    }
}
