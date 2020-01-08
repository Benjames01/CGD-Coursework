using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{

    // Experience
    [SerializeField]
    private int xpValue = 1;
    public int XpValue { get => xpValue; set => xpValue = value; }

    // Logic
    [SerializeField]
    private float triggerLength = 1;
    [SerializeField]
    private float chaseLength = 5;

    public float TriggerLength { get => triggerLength; set => triggerLength = value; }
    public float ChaseLength { get => chaseLength; set => chaseLength = value; }

    [SerializeField]
    private bool chasing;
    private bool collidingWithPlayer;
    [SerializeField]
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    [SerializeField]
    private ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    public void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    protected override void Start()
    {
        base.Start();
        
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected void FixedUpdate()
    {
        // Is in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) <= chaseLength)
        {
           
            if (Vector3.Distance(playerTransform.position, startingPosition) <= triggerLength)
                chasing = true;

            if (chasing && !collidingWithPlayer)
            {
                    UpdateMotor((playerTransform.position - transform.position).normalized);            
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        // Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            collidingWithPlayer = hits[i].tag == "Fighter" && hits[i].name == "Player";
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GainExp(xpValue);
        GameManager.instance.ShowText("+" + xpValue.ToString() + " exp", 25, Color.green, playerTransform.position, Vector3.up * 50, 1.5f);
    }

}
