using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    protected float speed;

    protected virtual void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if(collision.tag == "Collidable")
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Despawner")
        {
            Destroy(this.gameObject);
        }
    }
}
