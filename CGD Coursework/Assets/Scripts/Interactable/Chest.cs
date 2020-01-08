using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    [SerializeField]
    private Sprite closedSprite;

    [SerializeField]
    private int coinAmount = 5;
    protected override void OnCollect()
    {

        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = closedSprite;

            GameManager.instance.Coins += coinAmount;
            GameManager.instance.ShowText("+ " + coinAmount + " coins!", 20, Color.yellow, transform.position, Vector3.up * 50, 1.5f );
        }

    }

}
