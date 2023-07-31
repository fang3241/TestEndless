using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Collectable : MonoBehaviour
{
    public Sprite sprite;
    public float speed;
    
    protected LevelController levelController;
    protected PlayerController playerController;

    private void Start()
    {
        levelController = GameManager.instance.levelController;
        playerController = levelController.player;
    }

    protected virtual void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime * levelController.speedScaling);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Despawner")
        {
            Debug.Log("trigger " + collision + " from " + gameObject.name);
        }

        if (collision.tag == "Collectable")
        {
            DestroyObject();
        }

        if (collision == playerController.GetComponent<Collider2D>())
        {
            OnCollect();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Despawner")
        {
            DestroyObject();
        }
        
    }

    private void OnCollect()
    {
        Debug.Log("Collected " + this.gameObject.name);
        HitEffect();
        DestroyObject();
    }

    protected virtual void HitEffect()
    {
        Debug.Log("Hit Effect Applied");
    }

    protected void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
