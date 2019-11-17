using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float mSpeed = 10f;

    [SerializeField]
    Text mCollectText;

    Rigidbody2D mRigidbody;

    Animator mAnimator;

    // Weapon shooting variables
    [SerializeField]
    GameObject mShotPrefab; // Prefab of each shot

    [SerializeField]
    float mShootSpeed; // Speed at which each shot travels
    [SerializeField]
    float mShotDelay = 0.5f;
    float mLastShot = 0f;


    

    public static int mCollectedAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>(); // Get the rigidbody from the player
        mAnimator = GetComponentInChildren<Animator>(); // Get the animator from the player
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if((shootHorizontal != 0 || shootVertical != 0) && Time.time > mLastShot + mShotDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            mLastShot = Time.time;
        }

        if(moveHorizontal != 0 || moveVertical != 0)
        {
            mAnimator.SetBool("Walking", true);
        } else
        {
            mAnimator.SetBool("Walking", false);
        }

        
        mRigidbody.velocity = new Vector3(moveHorizontal * mSpeed,
                                    moveVertical * mSpeed,
                                    0);

        mCollectText.text = "Items Collected: " + mCollectedAmount;

    }


    void Shoot(float x, float y)
    {
        GameObject shot = Instantiate(mShotPrefab, transform.position, transform.rotation) as GameObject;
        shot.AddComponent<Rigidbody2D>().gravityScale = 0;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * mShootSpeed : Mathf.Ceil(x) * mShootSpeed,
            (y < 0) ? Mathf.Floor(y) * mShootSpeed : Mathf.Ceil(y) * mShootSpeed,
            0
            );
    }

}
