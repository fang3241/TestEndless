using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        Debug.Log("Collected");
        Destroy(this.gameObject);
    }
}
