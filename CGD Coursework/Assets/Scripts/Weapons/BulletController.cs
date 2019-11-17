using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    float mLifeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyDelay());
        transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
    } 


    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(mLifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Die();
            Destroy(gameObject);
        }
    }

}
