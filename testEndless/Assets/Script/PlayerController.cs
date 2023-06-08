using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int playerLane;


    private int playerHP;
    private float posX = -7.4f;
    private bool isMoving;

    private LevelController LevelController;
    // Start is called before the first frame update
    void Start()
    {
        LevelController = GameObject.FindObjectOfType<LevelController>();
        playerHP = LevelController.hp;
        playerLane = 1;
        isMoving = false;
        this.transform.position = new Vector3(posX, Lane.playerPosition[playerLane],1);
        //Debug.Log(new Vector3(posX, Lane.playerPosition[playerPos], 1));
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHP != 0)
        {
            if (!isMoving)
            {
                if (Input.GetKeyDown(KeyCode.W) && playerLane > 0)
                {
                    StartCoroutine(Move(playerLane - 1, speed * LevelController.speedScaling));

                }
                else if (Input.GetKeyDown(KeyCode.S) && playerLane < 3)
                {
                    StartCoroutine(Move(playerLane + 1, speed * LevelController.speedScaling));

                }
            }
        }
        else
        {
            LevelController.Lose();
        }
        
        


    }

    public void Hit()
    {
        playerHP--;
        LevelController.hpIcoImg[playerHP].gameObject.SetActive(false);
    }

    public void Heal()
    {
        if (playerHP < 3)
        {
            LevelController.hpIcoImg[playerHP].gameObject.SetActive(true);
            playerHP++;
        }
    }

    IEnumerator Move(int laneDest, float speed)
    {
        while(transform.position.y != Lane.playerPosition[laneDest])
        {
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, 
                new Vector2(transform.position.x, Lane.playerPosition[laneDest]), speed * Time.deltaTime);
            yield return null;
        }
        playerLane = laneDest;
        isMoving = false;
    }
}
