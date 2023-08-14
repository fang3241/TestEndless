using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSpawner : MonoBehaviour
{
    public GameObject parent;
    public GameObject answerPrefab;
    public List<Sprite> optionIcon;
    
    private LevelController levelController;

    public GameObject[] tempHolder;

    private char[] optionList = { 'A', 'B', 'C', 'D' };

    private void Awake()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
    }
    
    public void SpawnAnswer()
    {
        tempHolder = new GameObject[4];
        for(int i = 0; i < 4; i++)
        {
            GameObject temp = Instantiate(answerPrefab, LaneCord(i), transform.rotation, parent.transform);
            temp.name = "opsi-" + optionList[i];
            temp.GetComponent<SpriteRenderer>().sprite = optionIcon[i];
            temp.GetComponent<AnswerObj>().option = optionList[i];

            tempHolder[i] = temp;
        }
    }

    public void ClearAllAnswer()
    {
        foreach(GameObject a in tempHolder)
        {
            if(a != null)
            {
                Destroy(a);
            }
        }
    }

    private Vector3 LaneCord(int lane)
    {
        return new Vector2(transform.position.x, Lane.objectPosition[lane]);
    }

    //public void SpawnAns()
    //{
    //    SpawnAnswer(prefab, optionIcon);
    //}

    //protected override void Update()
    //{
    //    spawnInterval = ansSpawnInterval;
    //    if (Time.time > spawnTime)
    //    {
    //        if (!isSpawned)
    //        {
    //            OptionSetter();
    //        }
    //        spawnTime = Time.time + (spawnInterval / levelController.speedScaling);
    //    }
    //}

    //public void OptionSetter()
    //{
    //    SpawnAnswer(prefab, optionIco);
    //    isSpawned = true;
    //}


}
