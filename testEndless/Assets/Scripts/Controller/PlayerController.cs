using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     * Player Controller
     * player bergerak sesuai lane (0,1,2,3), default = 1
     * pos x buat seberapa jauh jarak dari kiri layar
     * 
     * koor lane ada di script Lane
     */


    private Transform playerTransform;
    public GameObject hurtPanel, healPanel;
    public float speed;
    public int playerLane;


    public int playerHP;
    private float posX = -7.4f;
    private bool isMoving;

    private LevelController LevelController;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        LevelController = GameObject.FindObjectOfType<LevelController>();
        playerHP = LevelController.hp;
        playerLane = 1;
        isMoving = false;
        this.transform.position = new Vector3(posX, Lane.playerPosition[playerLane],1);
    }
    
    void Update()
    {
        if(playerHP != 0)
        {
            if (!isMoving)
            {
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && playerLane > 0)
                {
                    Debug.Log("UP");
                    StartCoroutine(Move(playerLane - 1, speed * LevelController.speedScaling, 5));
                }
                else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && playerLane < 3)
                {
                    Debug.Log("Down");
                    StartCoroutine(Move(playerLane + 1, speed * LevelController.speedScaling, -5));
                }

            }
        }
        else
        {
            LevelController.Lose();
        }
        
    }

    public void Hit(int dmg)
    {//tambahkan batasan hp, biar gk out of bounds
        StartCoroutine(EventPanel(0));
        playerHP -= dmg;
        LevelController.hpIcoImg[playerHP].gameObject.SetActive(false);
        LevelController.objectiveController.objectives[2].addProgress();
    }

    public void Heal(int addHp)
    {
        if (playerHP < 3)
        {
            LevelController.hpIcoImg[playerHP].gameObject.SetActive(true);
            StartCoroutine(EventPanel(1));
        }
    }

    IEnumerator Move(int laneDest, float speed, int dir)
    {
        while(transform.position.y != Lane.playerPosition[laneDest])
        {
            isMoving = true;
            playerTransform.rotation = Quaternion.Euler(0, 15, dir);
            transform.position = Vector2.MoveTowards(transform.position, 
                new Vector2(transform.position.x, Lane.playerPosition[laneDest]), speed * Time.deltaTime);
            yield return null;
        }
        playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        playerLane = laneDest;
        isMoving = false;
    }

    IEnumerator EventPanel(int i)
    {
        if(i == 0)
        {
            hurtPanel.SetActive(true);
            EZCameraShake.CameraShaker.Instance.ShakeOnce(4f, 4f, 0f, 0.4f);
            yield return new WaitForSeconds(0.3f);
            hurtPanel.SetActive(false);
        }else if(i == 1)
        {
            healPanel.SetActive(true);
            playerHP++;
            yield return new WaitForSeconds(0.3f);
            healPanel.SetActive(false);
        }
        yield return null;
        
    }
}
