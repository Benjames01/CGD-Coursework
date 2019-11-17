using UnityEngine;

public class CollectionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision: " + collision.tag);

        if(collision.tag == "Player")
        {
            PlayerController.mCollectedAmount++;
            Destroy(gameObject);
        }

    }
}
