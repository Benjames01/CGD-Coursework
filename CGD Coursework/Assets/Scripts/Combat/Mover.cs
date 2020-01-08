using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{

    private Vector3 originalSize;

    protected BoxCollider2D boxCollider;

    protected Vector3 moveDelta; // Difference between current position and target

    protected RaycastHit2D hit;

    [SerializeField]
    protected float ySpeed = 0.75f;
    [SerializeField]
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Change direction of sprite, dependent on going left or right
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

        // If there is a push vector add it
        moveDelta += pushDirection;

        // Reduce push force every frame based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, PushRecoverySpeed);


        //V ertical Movement
        // Check we can move in this direction by casting a box there first, if null direction is available
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));

        // If we hit something its either on the 'Actor' or 'Blocking' layer so we don't want to move
        if (hit.collider == null)
        {
            // Move character
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }


        // Horizontal Movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

}
