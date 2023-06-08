using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerObj : Collectable
{
    public char option;
    public float objSpeed;
    private LevelController levelController;


    private void Start()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
    }

    protected override void Update()
    {
        speed = objSpeed * levelController.speedScaling;
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            levelController.selectedAnswer = option;
            levelController.AnswerChecker();
        }
        base.OnTriggerEnter2D(collision);
    }
    
}
