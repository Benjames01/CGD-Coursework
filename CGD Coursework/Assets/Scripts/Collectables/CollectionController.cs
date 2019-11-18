using UnityEngine;

public class CollectionController : MonoBehaviour
{

    [SerializeField]
    Item item;

    [SerializeField]
    float mHealthBoost;
    [SerializeField]
    float mMoveSpeedBoost;
    [SerializeField]
    float mAttackSpeedBoost;
    [SerializeField]
    float mBulletSizeBoost;


    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.ItemImage;
        Destroy(GetComponent<CircleCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController.collectedAmount++;
            GameController.BoostBulletSize(mBulletSizeBoost);
            GameController.BoostFireRate(mAttackSpeedBoost);
            GameController.BoostSpeed(mAttackSpeedBoost);
            GameController.HealPlayer(mHealthBoost);
            Destroy(gameObject);
        }
    }
}
