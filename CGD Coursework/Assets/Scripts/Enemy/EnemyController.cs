using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    GameObject mPlayer;

    EnemyState mState = EnemyState.Walk;
    [SerializeField]
    EnemyClass mClass;

    [SerializeField]
    float mRange = 10f;
    [SerializeField]
    float mSpeed = 1f;
    [SerializeField]
    float mAttackRange = 2f;
    [SerializeField]
    float mAttackDelay = 1f;
    [SerializeField]
    float mProjectileSpeed = .2f;

    [SerializeField]
    int mDamage = 1;

    bool mChooseDirection = false;
    bool mDead = false;
    bool mAttackCooldown;

    [SerializeField]
    GameObject mProjectilePrefab;

    Vector3 mRandomDirection;
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        switch (mState)
        {
            case (EnemyState.Walk):
                Walk();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                Die();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

        if (IsPlayerInRange(mRange) && mState != EnemyState.Die)
        {
            mState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(mRange) && mState != EnemyState.Die)
        {
            mState = EnemyState.Walk;
        }

        if (IsPlayerInRange(mAttackRange))
        {
            mState = EnemyState.Attack;
        }

    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, mPlayer.transform.position) <= range;
    }

    IEnumerator ChooseDirection()
    {
        mChooseDirection = true;

        yield return new WaitForSeconds(Random.Range(1.8f, 8.4f));

        mRandomDirection = new Vector3(0, 0, Random.Range(0, 360));

        Quaternion nextRotation = Quaternion.Euler(mRandomDirection);

        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.6f));

        mChooseDirection = false;
    }

    void Walk()
    {
        if (!mChooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * mSpeed * Time.deltaTime;

        if (IsPlayerInRange(mRange))
        {
            mState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            mPlayer.transform.position,
            mSpeed * Time.deltaTime);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void Attack()
    {
        if (!mAttackCooldown)
        {

            switch(mClass){

                case (EnemyClass.Melee):
                    GameController.DamagePlayer(mDamage);
                    StartCoroutine(AttackCooldown());
                    break;

                case (EnemyClass.Ranged):
                    GameObject projectile = Instantiate(mProjectilePrefab,
                                                        transform.position,
                                                        Quaternion.identity) as GameObject;

                    projectile.GetComponent<BulletController>().GetPlayer(mPlayer.transform);
                    projectile.AddComponent<Rigidbody2D>().gravityScale = 0;
                    projectile.GetComponent<BulletController>().IsEnemyBullet = true;
                    projectile.GetComponent<BulletController>().Speed = mProjectileSpeed;

                    StartCoroutine(AttackCooldown());
                    break;
            }


        }
        
    }

    private IEnumerator AttackCooldown()
    {
        mAttackCooldown = true;
        yield return new WaitForSeconds(mAttackDelay);
        mAttackCooldown = false;
    }
}

public enum EnemyState
{
    Walk,
    Follow,
    Die,
    Attack
};

public enum EnemyClass
{
    Melee,
    Ranged
};