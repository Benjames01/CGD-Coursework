using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    float mLifeTime = 0.5f;
    [SerializeField]
    float mSpeed;

    [SerializeField]
    bool mIsEnemyBullet = false;

    Vector2 mPreviousPos;
    Vector2 mCurrentPos;
    Vector2 mPlayerPos;
    public bool IsEnemyBullet { get => mIsEnemyBullet; set => mIsEnemyBullet = value; }
    public float Speed { get => mSpeed; set => mSpeed = value; }

    void Start()
    {
        StartCoroutine(DestroyDelay());
        if (!mIsEnemyBullet)
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize) * transform.localScale;        
    }

    void Update()
    {
        if (IsEnemyBullet)
        {
            mCurrentPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, mPlayerPos, mSpeed * Time.deltaTime);

            if (mCurrentPos == mPreviousPos)
                Destroy(gameObject);

            mPreviousPos = mCurrentPos;
        }
    }


    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(mLifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && !mIsEnemyBullet)
        {
            collision.gameObject.GetComponent<EnemyController>().Die();
            Destroy(gameObject);
        } 

        if(collision.tag == "Player" && IsEnemyBullet)
        {
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }
    }

    public void GetPlayer(Transform player)
    {
        mPlayerPos = player.position;
    }

}
