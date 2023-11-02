using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject parent;
    public GameObject itemHPPrefab;
    public GameObject itemBarPrefab;

    public float spawnTime;


    public bool canSpawn;
    private void Start()
    {
        canSpawn = true;
        StartCoroutine(SpawnTimer(spawnTime));
    }

    public void SpawnCollectable()
    {
        if (canSpawn)
        {
            if(Random.value > 0.5f)
            {
                Instantiate(itemHPPrefab, LaneCord(Random.Range(0, 4)), transform.rotation, parent.transform);
            }
            else
            {
                Instantiate(itemBarPrefab, LaneCord(Random.Range(0, 4)), transform.rotation, parent.transform);
            }
        }
        
        StartCoroutine(SpawnTimer(spawnTime));
    }

    private Vector3 LaneCord(int lane)
    {
        return new Vector2(transform.position.x, Lane.objectPosition[lane]);
    }

    //public void SpawnAtLane(GameObject obj, int lane)
    //{
    //    Instantiate(obj, LaneCord(lane), transform.rotation);
    //}
    public IEnumerator SpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnCollectable();

    }

}
