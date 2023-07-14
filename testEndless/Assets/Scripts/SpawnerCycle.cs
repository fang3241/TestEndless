using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCycle : ObjectSpawner//anggep aja ini obstacle spawner
{
    public GameObject prefab;
    public GameObject parent;
   
    public float spawnInterval;

    private float spawnTime;
    private LevelController levelController;

    private void Awake()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
        spawnInterval = 1;
    }
    
    private void Update()
    {
        if (Time.time > spawnTime)
        {
            if (!levelController.isQuestionSpawned)
            {
                SpawnObject(prefab, parent);
            }
            spawnTime = Time.time + (spawnInterval / levelController.speedScaling);
        }
    }
}
