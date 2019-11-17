using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    GameObject mPlayer;

    public EnemyState mState = EnemyState.Walk;

    [SerializeField]
    float mRange = 3f;
    [SerializeField]
    float mSpeed = 5f;

    bool mChooseDirection = false;

    bool mDead = false;

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
        }

        if (IsPlayerInRange(mRange) && mState != EnemyState.Die)
        {
            mState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(mRange) && mState != EnemyState.Die)
        {
            mState = EnemyState.Walk;
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


}

public enum EnemyState
{
    Walk,
    Follow,
    Die
};