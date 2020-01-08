using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : Weapon
{
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject magicPrefab;

    [SerializeField]
    private int maxWeaponLevel= 2;

    [SerializeField]
    private float[] attackSpeeds = { 15f, 20f, 25f };


    [SerializeField]
    private float[] lifeTime = { 3f, 4f, 5f };
    private float timeSpawned;


    private GameObject spawnedAttack;

    private bool spawned = false;

    private Vector2 mousePos;
    protected override void Start()
    {
        magicPrefab = GameManager.instance.MagicAttackPrefab[WeaponLevel];
        timeSpawned = -lifeTime[WeaponLevel];
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;

        // Find angle and convert from radians to degrees
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;

        rb.rotation = angle;
    }
    protected override void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (spawned)
        {
            BoxCollider = spawnedAttack.GetComponent<BoxCollider2D>();
            BoxCollider.OverlapCollider(Filter, Hits);

            for (int i = 0; i < Hits.Length; i++)
            {
                if (Hits[i] == null)
                    continue;

                OnCollide(Hits[i]);

                Hits[i] = null;
            }
        }

        if (Time.time - timeSpawned > lifeTime[WeaponLevel] && spawnedAttack != null)
        {
            GameObject.Destroy(spawnedAttack);
            spawned = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time - LastSwing > Cooldown && spawnedAttack == null)
            {
                LastSwing = Time.time;
                Attack();
            }
        }
    }


    protected override void OnCollide(Collider2D col)
    {
        if (col.tag == "Fighter" || col.tag == "Minion" || col.name == "Hitbox")
        {
            if (col.name == "Player" || col.tag == "Minion")
            {
                Physics2D.IgnoreCollision(col, BoxCollider);
                return;
            }

            Damage dmg = new Damage
            {
                DamageAmount = DamagePoint[WeaponLevel],
                Origin = transform.position,
                PushForce = PushForce[WeaponLevel]
            };
       
            if (col.name == "Hitbox")
            {
                Debug.Log("Hitbox");
                col.GetComponentInParent<Enemy>().SendMessage("ReceiveDamage", dmg);
                return;
            } 

            col.SendMessage("ReceiveDamage", dmg);
            
        }

        GameObject.Destroy(spawnedAttack);
        spawned = false;
    }


    protected override void Attack()
    {
        timeSpawned = Time.time;

        spawned = true;
        spawnedAttack = Instantiate(magicPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0,0,180));

        Rigidbody2D rb = spawnedAttack.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * attackSpeeds[WeaponLevel], ForceMode2D.Impulse);
    }

    public override void UpgradeWeapon()
    {
        
        WeaponLevel += 1;
        if (WeaponLevel > maxWeaponLevel)
            WeaponLevel = maxWeaponLevel;

        magicPrefab = GameManager.instance.MagicAttackPrefab[WeaponLevel];
    }

}
