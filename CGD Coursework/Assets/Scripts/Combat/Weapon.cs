using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    [SerializeField]
    private int[] damagePoint = {1, 2, 4, 7, 9, 11, 13};
    [SerializeField]
    private float[] pushForce = { 2.0f, 2.2f, 2.5f, 2.9f, 3.6f, 4f, 4.7f};

    // Upgrade
    [SerializeField]
    private int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Attack
    [SerializeField]
    private float cooldown = 0.5f;
    [SerializeField]
    private float lastSwing;

    private Animator anim;

    public int WeaponLevel { get => weaponLevel; set => weaponLevel = value; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float LastSwing { get => lastSwing; set => lastSwing = value; }
    public int[] DamagePoint { get => damagePoint; set => damagePoint = value; }
    public float[] PushForce { get => pushForce; set => pushForce = value; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();  
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - LastSwing > Cooldown)
            {
                LastSwing = Time.time;
                Attack();
            }
        }
    }

    protected virtual void Attack()
    {
        anim.SetTrigger("Swing");
    }

    protected override void OnCollide(Collider2D col)
    {
        if (col.tag == "Fighter")
        {
            if (col.name == "Player")
                return;
            Damage dmg = new Damage
            {
                DamageAmount = DamagePoint[weaponLevel],
                Origin = transform.position,
                PushForce = PushForce[weaponLevel]
            };

            col.SendMessage("ReceiveDamage", dmg);
        }    
    }

    public virtual void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.WeaponSprites[weaponLevel];
    }
}
