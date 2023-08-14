using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerObj : Collectable
{
    public char option;
    
    protected override void HitEffect()
    {
        Debug.Log(option + " Hit");

        levelController.answerSpawner.ClearAllAnswer();
        levelController.selectedAnswer = option;
        levelController.Answered();
    }



    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Player")
    //    {
    //        levelController.selectedAnswer = option;
    //        levelController.AnswerChecker();
    //    }

    //    base.OnTriggerEnter2D(collision);
    //}

}
