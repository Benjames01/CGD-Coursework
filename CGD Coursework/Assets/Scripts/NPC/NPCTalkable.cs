using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkable : Collidable
{
    [SerializeField]
    private string message;
    [SerializeField]
    private float cooldown = 4.0f;
    private float lastShown = -4.0f;




    protected override void OnCollide(Collider2D col)
    {
        base.OnCollide(col);
        if(Time.time - lastShown > cooldown && col.name == "Player")
        {
            lastShown = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white,
                transform.position + new Vector3(0, 0.16f, 0),
                Vector3.zero, cooldown);
        }



    }
}
