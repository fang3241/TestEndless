using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSpawner : ObjectSpawner
{
    public GameObject prefab;
    public List<Sprite> optionIcon;
    public bool isSpawned;

    public float ansSpawnInterval;

    private void Start()
    {
        isSpawned = false;
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
