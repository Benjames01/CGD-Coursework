using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

    public static GameController instance;

    static float health = 6;
    static float maxHealth = 6;
    static float moveSpeed = 5f;
    static float fireRate = 0.5f;
    static float bulletSize = 1f;

    [SerializeField]
    private Text mhealthText;

    public static float Health { get => health; set => health = value; }
    public static float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if(mhealthText != null)
            mhealthText.text = "Health: " + health;
    }

    public static void DamagePlayer(int damage)
    {

        health -= damage;

        if (Health <= 0)
        {
            KillPlayer();
        }

    }

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void BoostSpeed(float speed)
    {
        moveSpeed += speed;
    }

    public static void BoostFireRate(float rate)
    {
        fireRate -= rate; // Smaller the rate, faster the bullets spawn
    }
    public static void BoostBulletSize(float size)
    {
        bulletSize += size;
    }

    private static void KillPlayer()
    {

    }
}
