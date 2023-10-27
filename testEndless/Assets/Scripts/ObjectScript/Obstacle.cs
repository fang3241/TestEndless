using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Collectable
{
    public int Damage;
    public Animator anim;


    private void OnDisable()
    {
        //Debug.Log("Destroyed");
        //StartCoroutine(Destroy());
    }
    
    IEnumerator Destroy()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Destroy");
        yield return new WaitForSeconds(0.05f);
        Destroy(this);
    }

    protected override void HitEffect()
    {
        playerController.Hit(Damage);
    }



    //public float obsSpeed;
    //private LevelController levelController;
    //private PlayerController player;

    //private void Start()
    //{
    //    levelController = GameObject.FindObjectOfType<LevelController>();
    //    player = levelController.player.GetComponent<PlayerController>();
    //}

    //protected override void Update()
    //{
    //    speed = obsSpeed * levelController.speedScaling;
    //    base.Update();
    //}

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //isi collision buat obstacle -> ngurang HP
    //    if(collision.tag == "Player")
    //    {
    //        //levelController.Hit();
    //        player.Hit();
    //        levelController.objectiveController.objectives[2].addProgress();
    //        //Debug.Log(collision.name);
    //        Destroy(this.gameObject);
    //    }
    //    base.OnTriggerEnter2D(collision);
    //}
}
