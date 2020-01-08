using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{

    [SerializeField]
    private ContactFilter2D filter;

    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    public BoxCollider2D BoxCollider { get => boxCollider; set => boxCollider = value; }
    public Collider2D[] Hits { get => hits; set => hits = value; }
    public ContactFilter2D Filter { get => filter; set => filter = value; }

    protected virtual void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Collisions
        BoxCollider.OverlapCollider(Filter, Hits);

        for (int i = 0; i < Hits.Length; i++)
        {
            if(Hits[i] == null)
                continue;

            OnCollide(Hits[i]);

            Hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D col)
    {
        Debug.Log("OnCollide has not been implemented in: " + this.name);
    }


}
