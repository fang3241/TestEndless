using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSpawner : ObjectSpawner
{
    public GameObject prefab;
    public List<Sprite> optionIcon;

    public float ansSpawnInterval;

    private LevelController levelController;

    private void Awake()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
    }

    private void Start()
    {
        
    }

    public void SpawnAns()
    {
        SpawnAnswer(prefab, optionIcon);
    }

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
