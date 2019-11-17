using UnityEngine;

public class CollectionController : MonoBehaviour
{

    Item item;

    float mHealthBoost;
    float mMoveSpeedBoost;
    float mAttackSpeedBoost;
    float mBulletSizeBoost;


    void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = item.ItemImage;
        //Destroy(GetComponent<PolygonCollider2D>());
       //gameObject.AddComponent<PolygonCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision: " + collision.tag);

        if(collision.tag == "Player")
        {
            PlayerController.collectedAmount++;
            Destroy(gameObject);
        }

    }
}
